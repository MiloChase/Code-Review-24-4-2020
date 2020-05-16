# Code-Review-24-4-2020
CS 78 Capstone (USLI OSU AIAA)



The GUI Payload can be ran or viewed by opening microsoft visual (not studio) and clicking run at the top or by selecting run on the .csproj.



### Avionics GUI
The Avionics GUI can be ran by downloading the GUI folder and running the following command:

#### Make sure Python | PIP | virtualenv are all installed or else this won't work. 

* python -m venv env
* virtualenv .env
* source .env/bin/activate
* cd GUI
* pip install -r requirements.txt
* python3 gui.py

This will run the GUI and display the graph with the acceleration of the launch vehicle. Once you close the GUI, your default browser will open up with the plots on a map showcasing the locations of the launch vehicle.
