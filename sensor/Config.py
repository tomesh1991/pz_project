import os
from random import randint

class Config:
    def __init__(self):
        self.hostUniqueId = os.popen('wmic baseboard get serialnumber').read().split()[1]
        print(self.hostUniqueId)
    frequency = 1.0/60.0
    name = "PIRATO_NINJA"
    sensorId = randint(0,10000000)
    monitorUrl = "http://localhost:52123/api/measurements"
    sensorType = "util/MEM" # util/MEM - pamiec util/CPU - %CPU