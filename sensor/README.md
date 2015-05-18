Najlepiej używać środowiska pyCharm community edition.

https://www.jetbrains.com/pycharm/download/

W pliku Config.py są ustawienia, uruchamiamy plik sensor.py

ID hosta jest generowane z unikalnego id płyty głównej komputera, to się raczej nie powtórzy.

Responder służy tylko do testów! Jeżeli będzie działał monitor NIE WŁĄCZAMY RESPONDERA!
Jeżeli chcemy potestować najpierw włączamy responder, potem sensor.py

UWAGA!!!
Ten sensor działa tylko pod windowsem, id płyty głównej wykrywa się inaczej pod linuchem.
Jeśli będzie trzeba zrobić też linucha proszę o info w jakim issue.

Potrzebne biblioteki:
requests