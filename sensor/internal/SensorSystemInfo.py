import os
import platform
import psutil 
import socket
from internal.SensorBase import *

class SensorSystemInfo(SensorBase):

    "Provides general information about system"

    def __init__(self, options):
        SensorBase.__init__(self,options)
        self.sensortype = "System Info"
        # add metrics' names and methods here        
        self.metrics = {
            'systemName' : self.name,
            #'IP' : self.IP, sth is fucked up
            'CPU' : self.CPU,
            'architecture' : self.architecture,
            'totalRAM' : self.totalRAM,
            'totalDiskSpace' : self.totalDiskSpace
        }
    # Return system name f.e. 'Windows 7'
    def name(self):
        return platform.system() + " " + platform.release()
        
    # Return my IP
    # Doesn't work with hostnames where are polish characters 
    # ... and generally doesn't work ^_^
    def IP(self):
        return socket.gethostbyname(socket.gethostname())

        
    # Return CPU name
    def CPU(self): 
        return platform.processor()
        
    # Return architecture (32bit, 64bit)
    def architecture(self):
        return platform.architecture()[0]

    # Return total amount of installed RAM in bytes.
    def totalRAM(self):
        mem = psutil.virtual_memory()
        return int(mem.total)
        
    # Return total size of disks in bytes.
    def totalDiskSpace(self):
        return psutil.disk_usage('/').total
