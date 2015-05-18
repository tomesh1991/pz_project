import os
from random import randint

class Config:
    def __init__(self):
        self.hostUniqueId = os.popen('wmic baseboard get serialnumber').read().split()[1]
        print(self.hostUniqueId)
    frequency = 1.0/60.0
    monitorUrl = "http://localhost:1548/api/measurements"
    name = "SUPAKOMPJUTA"
    sensorId = randint(0,10000000)
    # util/MEM - %pamięć, util/CPU - %CPU
    sensorType = "util/MEM"