import psutil 
import threading
import time
from internal.SensorBase import *

class SensorNetworkInfo(SensorBase):

    "Provides information about network"
    
    lastBytesSend = 0 
    lastBytesReceived = 0 
    kbitsPerSecondOut = 0
    kbitsPerSecondIn = 0
    statisticsPerSecondThread = None
    runEvent = None

    isFirstRunKBitsPerSecondIn = True
    isFirstRunKBitsPerSecondOut = True

    def __init__(self, options):
        SensorBase.__init__(self,options)
        self.sensortype = "Network Info"
        self.lastBytesReceived = self.bytesReceived()
        self.lastBytesSent = self.bytesSent()
        statisticsPerSecondThread = threading.Thread(target = self.__gatherStatistics)
        statisticsPerSecondThread.start()
        # add metrics' names and methods here        
        self.metrics = {
            'kBitsPerSecondOut' : self.kBitsPerSecondOut,
            'kBitsPerSecondIn' : self.kBitsPerSecondIn,
            'bytesSent' : self.bytesSent,
            'bytesReceived' : self.bytesReceived,
            'packetsSent' : self.packetsSent,
            'packetsReceived' : self.packetsReceived
        }

    # Return bytes send
    def bytesSent(self):
        return psutil.net_io_counters().bytes_sent
        
    # Return bytes received
    def bytesReceived(self):
        return psutil.net_io_counters().bytes_recv
        
    # Return packets sent
    def packetsSent(self):
        return psutil.net_io_counters().packets_sent
        
    # Return packets received
    def packetsReceived(self):
        return psutil.net_io_counters().packets_recv

    # Return network load - kbits/s out
    def kBitsPerSecondOut(self):
        if self.isFirstRunKBitsPerSecondOut:
            self.isFirstRunKBitsPerSecondOut = False
            return 0

        return self.kbitsPerSecondOut

    # Return network load - kbits/s in
    def kBitsPerSecondIn(self):
        if self.isFirstRunKBitsPerSecondIn:
            self.isFirstRunKBitsPerSecondIn = False
            return 0

        return self.kbitsPerSecondIn
    
    # Gather statistics with interval of 1.0 second
    def __gatherStatistics(self):
        while self.isRunning:
            
            # kbits/s in

            recv = self.bytesReceived() 
            self.kbitsPerSecondIn = (recv - self.lastBytesReceived) * 8.0 / 1024.0
            self.lastBytesReceived = recv

            # kbits/s out
            sent = self.bytesSent() 
            self.kbitsPerSecondOut = (sent - self.lastBytesSend) * 8.0 / 1024.0
            self.lastBytesSend = sent

            time.sleep(1)

        
