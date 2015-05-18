from Config import *
from SensorUtil import *


if __name__ == "__main__":
    config = Config()
    sensor = SensorUtil(config)

    sensor.isRunning = True
    sensor.run()
    sensor.stop()
    print("\n UPS...\n")