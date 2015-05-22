import psutil
from time import sleep
import json
import requests
from uuid import getnode as get_mac

class SensorUtil():
    interval = 0.0
    isRunning = True
    msg = {}
    dataToMeas = {}
    url = ''
    headers = {'Content-Type': 'application/json'}

    def __init__(self, config):
        self.config = config
        self.url = self.config.monitorUrl
        print(self.url)
        self.interval = 1.0 / config.frequency


    def stop(self):
        self.isRunning = False

    def run(self):
        print("Sensor: " + self.config.sensorType + "\n")
        self.measureData()

    def measureData(self):
        while self.isRunning:
            value = None
            if self.config.sensorType == "util/MEM":
                value = self.MEM()
                self.config.sensorId = 2568157
            if self.config.sensorType == "util/CPU":
                value = self.CPU()
                self.config.sensorId = 12321
            data = {
                "host": {
                    "name": self.config.name,
                    "unique_id": self.config.hostUniqueId
                },
                "SensorUniqueId": self.config.sensorId,
                "Value": value
            }
            try:
                r = requests.post(self.url, data=json.dumps(data), headers=self.headers, )
            except:
                print("Monitor is not responding, next connection attempt in 10 seconds\n")
                sleep(10.0)
                continue
            print(repr(data) + " Sent...")
            print(r.status_code)

            sleep(self.interval)

    def CPU(self):
        return int(psutil.cpu_percent())

    def MEM(self):
        return int(psutil.virtual_memory().percent)