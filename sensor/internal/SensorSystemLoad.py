import os
import psutil 
from internal.SensorBase import *
import internal.SensorSystemInfo
import requests
from uuid import getnode as get_mac

class SensorSystemLoad(SensorBase):

    "Provides information about system load"
    def __init__(self, options):
        self.i = 0
        SensorBase.__init__(self,options)
        self.sensortype = "System Load"
        # add metrics' names and methods here        
        self.metrics = {
            'freeMemory' : self.freeMemory,
            'cpuUtilization' : self.cpuUtilization
        }
        # TUTAJ ROBIMY POSTA
        url = 'http://localhost:1548/api/measurements'
        data = {
            "id": self.i,
            "host_id": repr(get_mac()),
            "timestamp": repr(time.time()),
            "load_cpu": repr(self.cpuUtilization),
            "load_mem": repr(self.freeMemory),
            "hosts": {
                "id": repr(get_mac()),
                "name": options.hostname,
                "ip_addr": "127.0.0.1",
                "status": "running",
                "measurements": []
            }
        }
        headers = {'Content-Type': 'application/json'}

        r = requests.post(url, data=json.dumps(data), headers=headers)
        print(r.headers)
    
    # Return free memory in bytes
    def freeMemory(self):
        return psutil.virtual_memory().free
        
    # Return system-wide CPU utilization as float
    def cpuUtilization(self):
        return psutil.cpu_percent(0.2)


