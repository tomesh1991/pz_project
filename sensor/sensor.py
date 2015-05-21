import sys
import getopt
import json
import os.path
import socket
from pprint import *
from internal.Options import *
from internal.SensorBase import *
from internal.SensorNetworkInfo import *
from internal.SensorSystemInfo import *
from internal.SensorSystemLoad import *
from urllib.request import *
import requests
from urllib.response import *
from urllib.parse import *

def usage():
    print("sensor.py TYPE [OPTION]... [CONFIG FILE]")
    print("")
    print("TYPE:")
    print("\tSystemInfo")
    print("\tSystemLoad")
    print("\tNetworkInfo")
    print("")

    print("OPTION:")
    print("\t-h,--help\t\tShow usage")
    
    print("\t-f,--frequency=FLOAT\tSet sensor frequency")
    print("\t-m,--monitor-ip=IP\tSet monitor IP address")
    print("\t-d,--port=INT\t\tSet UDP port")
    print("\t-n,--hostname=STRING\tSet host name")
    print("\t-s,--sensorname=STRING\tSet sensor name")
    print("\t-u,--username=STRING\tSet username")
    print("\t-p,--password=STRING\tSet password")

    
def load(filename):
    if not os.path.isfile(filename):
        print("File doesn't exists \"" + filename + "\"");
        return [0,"","","","",""]
        
    jsonData=open(filename)
    data = json.load(jsonData)
    
    print("====================================")
    print("Settings:")
    print("====================================")
    pprint(data)
    jsonData.close()
    print("====================================")
    options = Options()    
    try:
        options.frequency = data["frequency"]
        options.monitorIP = data["server_ip"]
        options.port = data["server_port"]
        options.hostname = data["hostname"]
        options.sensorname = data["sensorname"]
        options.username = data["username"]
        options.password = data["secret"]
        options.config_file = filename
    except KeyError as err:
        print ("No key " + str(err) + "in config file")
        sys.exit(2)
        
    return options
    

def main(argv):

    try:
        opts, args = getopt.getopt(argv[1:], "hf:m:d:n:s:u:p:", ["help", "frequency=","monitor-ip=","port=","hostname=","sensorname=","username=","password="])
    except getopt.GetoptError as err:
        print(err)
        usage()
        sys.exit(2)

    options = Options()

    # Check if filename is given when no options
    if len(args) == 0 and len(opts) == 0:
        usage()
        sys.exit()

    # Check if type is given
    if(argv[0] != "SystemInfo" and argv[0] != "NetworkInfo" and argv[0] != "SystemLoad"):    
        print("Sensor type not recognized")
        sys.exit()
        

        
    # Load config file
    if len(args) == 1:
        options = load(args[0])

    # Set sensor type
    options.sensortype = argv[0]
        
    # Parse options
    for o,value in opts:
        if o in ("-h", "--help"):
            usage()
            sys.exit()
        elif o in ("-f", "--frequency"):
            options.frequency = value
        elif o in ("-m", "--monitor-ip"):
            options.monitorIP = value
        elif o in ("-d", "--port"):
            options.port = value
        elif o in ("-n", "--hostname"):
            options.hostname = value
        elif o in ("-s", "--sensorname"):
            options.sensorname = value
        elif o in ("-u", "--username"):
            options.username = value
        elif o in ("-p", "--password"):
            options.password = value
        else:    
            print("Unhandled option")
            sys.exit(2)    
            
    # Frequency validation
    
    try:
        if float(options.frequency) <= 0:
            raise ValueError
    except ValueError:
        print("Frequency must be positive float")
        sys.exit(2)
        
    options.frequency = float(options.frequency)
    
    # Port validation
    
    try:
        if int(options.port) <= 0:
            raise ValueError
    except ValueError:
        print("Port must be positive integer")
        sys.exit(2)
        
    options.port = int(options.port)
    
    # Monitor IP validation
    
    try:
        socket.inet_aton(options.monitorIP)
    except socket.error:
        print("Invalid monitor IP")
        sys.exit(2)

    # Run sensor

    sensor = None
    
    if options.sensortype == "SystemInfo":
        sensor = SensorSystemInfo(options)
    if options.sensortype == "SystemLoad":
        sensor = SensorSystemLoad(options)
    if options.sensortype == "NetworkInfo":
        sensor = SensorNetworkInfo(options)
    
    try:
        sensor.run()
    except:
        sensor.stop()
        print("Unexpected error:", sys.exc_info())
        print("Closing...")
        

if __name__ == "__main__":
    main(sys.argv[1:])
