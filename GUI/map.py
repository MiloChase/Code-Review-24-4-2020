import plotly.graph_objects as go
import tkinter as tk
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import csv

# This will display the plots on a map.
# It will open up your default browser and show the map using mapbox.
# This function is called from the main function where it will be passed in the latitude and longitude from each reading.
def display(lat,lon, startLat, startLon):
    mapbox_access_token = open("mapbox_token").read()



    fig = go.Figure(go.Scattermapbox(
            lat=lat,
            lon=lon,
            mode='markers',
            marker=go.scattermapbox.Marker(
                size=14
            ),
            text=['Brothers Launch Site'],
        ))

    fig.update_layout(
        hovermode='closest',
        mapbox=dict(
            accesstoken=mapbox_access_token,
            bearing=0,
            center=go.layout.mapbox.Center(
                lat=float(startLat),
                lon=float(startLon)
            ),
            pitch=0,
            zoom=25
        )
    )

    fig.show()
