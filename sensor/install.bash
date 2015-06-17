declare -a arr=("CPU_sensor/" "MEM_sensor/")
declare -a arr2=("util/CPU" "util/MEM")

mkdir CPU_sensor;
mkdir MEM_sensor;

for ((a = 0 ; a < 2 ; a++ ));
do
   i=${arr[$a]}
   echo "$i"
   cp Config.py $i
   cp sensor.py $i
   cp SensorUtil.py $i
   i=$i"Config.py"
   printf "\n    monitorUrl = " >> $i 
   printf '"' >> $i
   printf $1 >> $i
   printf '"' >> $i
   printf "\n" >> $i
   printf "    sensorType = " >> $i 
   printf '"' >> $i
   printf ${arr2[$a]}>> $i
   printf '"' >> $i
   printf "\n" >> $i
   python.exe ${arr[$a]}"sensor.py"&
done


