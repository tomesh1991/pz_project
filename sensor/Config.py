from random import randint

class Config:
    frequency = 2.0
    monitorUrl = "http://localhost:1548/api/measurements"
    name = "SUPAKOMPJUTA"
    sensorUniqueId = repr(randint(0, 10000000))
    # util/MEM - %pamięć, util/CPU - %CPU
    sensorType = "util/MEM"