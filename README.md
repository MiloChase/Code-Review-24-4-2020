# Code-Review-24-4-2020
CS 78 Capstone (USLI OSU AIAA)

### Payload GUI

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

## Code Review Changes

### Payload GUI Changes

### Avionics GUI Changes

After the Code Review, there were a lot of good feedback that was given that would help teams in the future be able to understand our code. The changes that I made are:

* Added more comments
* Renamed Functions
* Virtual Environment

#### Comments
I added more comments that way readers are able to go through the code and be able to understand what each part is doing. 

#### Renamed Functions
Since I was assigned the Avionics GUI late in the Capstone year, I had poor functions name that only I would be able to understand. Instead of having that, I went through and changed some of the function names so that everyone will be able to have a better understanding of what each function is suppose to be doing. 

#### Virtual Environment
In the code review it was mentioned that when other teams tried to run the Python GUI, they did not seem the same results as I did and instead ran into errors. The reason for these errors were because these individuals needed to go through and download every Python library I installed to be able to make the GUI work. Instead of putting the burden on the reviewer to waste their time and find all the different libraries, I provided the steps to create a virtual environment where the reviewer does not have to waste anytime in downloading the libraries and can go straight to reviewing the code. 
