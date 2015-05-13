from Config import *
from SensorUtil import *


if __name__ == "__main__":
    config = Config()
    sensor = SensorUtil(config)

    try:
        sensor.isRunning = True
        sensor.run()
    except:
        sensor.stop()
        print("\n UPS...\n")