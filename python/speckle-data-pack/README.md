# Speckle Data Pack
Speckle Data Pack is a comprehensive content pack that has been designed to inspire curiosity and demonstrate a practical way to gain insights from your BIM data stored in Speckle. The aim of the content pack is to provide an intermediate-level resource for data-minded architects, engineers, and BIM managers who are looking to take their next step beyond Excel.

This training set provides a convenient way to work with sample BIM data. It is a playground for testing and experimenting, and for becoming familiar with Speckle schema. The pack includes a Speckle project (stream) with 30 Revit models and [Jupyter](https://jupyter.org/) notebook examples showing different ways of working with a large data set containing the whole project portfolio. Set of Revit models in the Speckle Data Pack project (stream) is a collection of simple buildings that vary in size and contain basic Revit types which makes it easy to play with and get results quickly.

You will learn how to load data from Speckle with [specklepy](https://github.com/specklesystems/specklepy) (our Python SDK), then how to convert it into tables and leverage [Pandas](https://pandas.pydata.org/) to analyse and manipulate BIM data. Bonus steps with [PandasAI](https://pandas-ai.readthedocs.io/en/latest/) show you how to integrate AI into you data workflows. OpenAI’s ChatGPT is used in examples but PandasAI offers more options for LLM so feel free to experiment!

Basic Python skills are required and some knowledge of Pandas is beneficial. Instructions how to set up Jupyter Notebooks in Visual Studio Code are [here](https://code.visualstudio.com/docs/datascience/jupyter-notebooks).
## Speckle Project

[Speckle Data Pack I](https://speckle.xyz/streams/729cb7c74b) 

It contains 30 simple buildings from Revit 2022, 2023 and 2024. Models are stored either as

- versions (commits) of a single model (branch) called `all_in_one`
- individual models (branches) named after the Revit files (SB2201, …)

## Code Examples

- SDP_1_room data from model.ipynb
- SDP_2_room data from project.ipynb
- SDP_3_room data with pandas.ipynb
- SDP_4_material data with pandas.ipnyb

## Files

Download all files [here](https://drive.google.com/file/d/1no_R9lgh5MP9SmSsHdVDXeFkqjWcri_Z/view?usp=sharing), in case you want to set up this content pack on your own server. 

- 30 Revit models
- Grasshopper definition that generated the BIM data set