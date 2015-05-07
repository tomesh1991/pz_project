from internal.Options import *
from time import sleep
import time
import threading
import json
#import client
class SensorBase:

    """Base class for all sensors"""

    interval = 0.0
    encoding = 'UTF-8'
    isRunning = True
    msg = {}
    udp_client = None
    metrics = {}
    sensortype = None

    def __init__(self, options):
        self.options = options
        self.interval = 1.0 / options.frequency
        # this is only common field in both register and data frame
        self.msg['sensor_name'] = self.getSensorName()

    def stop(self):
        self.isRunning = False

    # Main method - runs sensor logic
    def run(self):
        print("====================================")
        print("Hello, World!")
        print("My name is " + self.getSensorName())
        print("====================================")
        self.register()
        sleep(1)
        self.work()

    # Register sensor in monitor
    # If fail just exit
    def register(self):
        register_msg = self.msg.copy()
        register_msg['message_type'] = "register"
        register_msg['rpm'] = int(self.getFrequency()*60)
        register_msg['hostname'] = self.getHostName()
        register_msg['username'] = self.options.username
        register_msg['sensor_type'] = self.sensortype
        print(json.JSONEncoder().encode(register_msg))
        print("Sensor registered")

    # Virtual method - write here unique logic for every sensor
    def work(self):
        data_msg = self.msg.copy()
        while self.isRunning:
            for metric, method in self.metrics.items():
                data_msg['metrics_name'] = metric
                data_msg['dupa_daniela'] = time.timezone
                data_msg['message_type'] = "measurement"
                data_msg['data'] = {'val' : method(), 'time' : str(self.getTimestamp()) }
                json_msg = json.JSONEncoder().encode(data_msg)
                print(json_msg)
            print("Sending metrics...")
            sleep(self.interval)

    # Wrapper for sensor name
    def getSensorName(self):
        return self.options.sensorname;

    # Wrapper for host name
    def getHostName(self):
        return self.options.hostname;
    
    def getFrequency(self):
        return self.options.frequency;

    def getTimestamp(self):
        return int(time.time());

