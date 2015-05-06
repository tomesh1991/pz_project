from internal.SensorSystemInfo import *
from internal.SensorSystemLoad import *
from internal.SensorNetworkInfo import *
from internal.Options import *
import time
import os

clear = lambda: os.system('cls')


options = Options()
options.frequency = 1.0
networkInfo = SensorNetworkInfo(options)
systemInfo = SensorSystemInfo(options)
systemLoad = SensorSystemLoad(options)

try:
    while True:

        clear()

        print("========= SystemInfo =========")
        print("System name:\t\t%s" % systemInfo.name())
        print("Architecture:\t\t%s" % systemInfo.architecture())
        print("CPU name:\t\t%s" % systemInfo.CPU())
        print("Total RAM:\t\t%f GB" % (systemInfo.totalRAM() / 1024.0 / 1024.0 / 1024.0) )
        print("Total disk space:\t%f GB" % (systemInfo.totalDiskSpace() / 1024.0 / 1024.0 / 1024.0) )
        print("Host IP:\t\t%s" % systemInfo.IP())
        print("")
        print("========= SystemLoad =========")
        print("Free memory:\t\t%f MB" % (systemLoad.freeMemory() / 1024.0 / 1024.0) )
        print("CPU utilization:\t%0.2f%%" % systemLoad.cpuUtilization())

        print("")
        print("======== NetworkInfo =========")
        print("Bytes sent:\t\t%d" % networkInfo.bytesSent() )
        print("Bytes recv:\t\t%d" % networkInfo.bytesReceived() )
        print("Packets sent:\t\t%d" % networkInfo.packetsSent() )
        print("Packets recv:\t\t%d" % networkInfo.packetsReceived() )
        print("Network in: \t\t%d kbit/s" % networkInfo.kBitsPerSecondIn() )
        print("Network out: \t\t%d kbit/s" % networkInfo.kBitsPerSecondOut() )
        time.sleep(1)

except:
    networkInfo.isRunning = False
