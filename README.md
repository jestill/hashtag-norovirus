# Hashtag Norovirus

An experiment in using social media for public health surveillance. Tools and concepts used in this hack could include:

* Natural Language Processing
* Creation and consumption of APIs
* Visualzation and Analytics environments
* Data integration services
* Machine Lerning 

This is a proposed project for the University of Michigan 2016 [Hacks With Friends](http://cio.umich.edu/michigan-it/hacks-with-friends). This github repository will be a place to collaboratively develop code and documentation. Ideally we will expose a Restful ODATA service that could be consumed in multiple contexts. We could also choose to present and anlyze the data in a streaming analytics pipeline.

Initial documentation can take place on the [project wiki](https://github.com/jestill/hashtag-norovirus/wiki). 

## Potential Discrete Events to Model

In addition to just norovirus, we can also try to build a generic model that allows us to generally pick up signals from other discrete events. The goal would be to parameterize the model based on discrete events with known start and end times. These can include sports events, weather events, academic calendar events, and other geographically constrained events such as the Flint water crises.

* **Norovirus** - Can we identify and report on the recent spread of Norovirus on campus.
* **Flint Water Crises** - Can we identify signals of low water quality associated with the switch to Flint river as a water source.
* **UM Football Games** - Can we identify when UM had a football game, and can we distinguish home games from away games.
* **UM Basketball Games** - Can we identify whem UM has a basketball game, and can we distinguish between men's and women's games
* **Finals Week** - Can we identify when UM is having finals week and can we identify a metric of student stress around this.
* **Spring Break** - Can we identify the time of spring break based on social media listening.

## Basic Data Model

Identifying a basic data model for representation of the social medai data will help us get quickly started.
Ideally this would not be linked to a specific social media listening use case or single platform.
Jamie is currently thinking about something like:

* **person** - The real life person or other entity that is behind the online persona. A person is an indviduval real life person, and an entity could be the entity associated with the A person can have multiple online media personas. This colum provides a way to reconcile a person or entity that is known to have multiple online personas (twitter stream, instagram account, facebook account etc). This is a generic identifier should be anonomized to a hash.
* **personaID** - The online social presence of a person. Twitter account, instagram etc. This could also be an anonomized hash that is built from incoming data. Something line md5sum ( personaURI & enrypteKEYString ). Having an anonomized personaID would allow for publishing resources and services as anonymous data streams.
* **personaURI** - The URL identifying the online persona.
* **platform** - The name of the social media platform being used to present the persona. Ideally a URL that can be used as a URI to reference the platform.
* **messageURI** - Unique URI of the message. (ie. Twitter URL or something else).
* **timeOfMessage** - The time that the message was sent as a timestamp. (Should not be NULL).
* **placeOfMessage** - The location the message was sent from in GIS coordinates. (Could be NULL)
* **messageString** - The social media message string as text. (Should not be NULL).

## Resources

The following resources will potentially help with the hack.

### Public APIs

#### Twitter

Twitter APIs could provide a route to get streaming data.

#### Instagram

[Instagram API](https://www.instagram.com/developer/) - seems to assume developers are building non-automated APIs.

#### Facebook

Facebook has an API.

#### Google+

What is the API?.


### Natural Langauge Processing

There could be a component of natural language processing to this hack.

### Visualization and Analytics Environment

Visualization of the streamed data could allow for simple consumption of the data streams.

* [Tableau](http://www.tableau.com) - Is a commercial application for visual analytics that can accepte OData streams as input. 
* [JBoss Dashbuilder]() - Is an open source dashboarding and analytics environment that can support analytics of real time streaming data.

### Git and GitHub Resources

[Git](https://git-scm.com/) is a free and open source version control system designed to facilitate the distributed development of source code for software. [GitHub](https://github.com/) is a public web-based Git repository hosting service with over 10 million users. This service allows inviduals or communities of users to host and collaboartively develop software and related artifacts. GitHub has been used by numerous informatics and cyberinfrastructure initiatives including [OHDSI](https://github.com/OHDSI/), [iPlant/CyVerse](https://github.com/iPlantCollaborativeOpenSource/), [Galaxy](https://github.com/galaxyproject), and [NCIP](https://github.com/NCIP/).

The [LHSNet organization on GitHub](https://github.com/LHSNet) allows the LHS Network to share the technical resources that are required to support the research being conducted by the LHSNet research network.

* [Git for beginners](http://readwrite.com/2013/09/30/understanding-github-a-journey-for-beginners-part-1) - *High level overview of Git.*
* [Software Carpentry - Git Novice Materials](https://github.com/swcarpentry/git-novice) - *Software carpentry course materials for getting started with Git.*
* [Markdown Basics](https://help.github.com/articles/markdown-basics/) - *This is the text formatting used for rich text documents in GitHub.*
* [Git GUI - Source Tree](https://www.atlassian.com/software/sourcetree/overview/) - *An Atlassian product to make it easier to use Git.*
* [Git GUI - GitHub Desktop](https://desktop.github.com/) - *The Git GUI desktop developed by GitHub.*
* [Git Crash Course for SVN users](http://git.or.cz/course/svn.html) - *A crash course on command line Git for SVN users.*

