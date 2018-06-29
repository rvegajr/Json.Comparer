# Json.Comparer - Compare JTokens, JObjects, JArrays and 
I have created Json.Comparer in order to allow for easy comparison of any object in .net and 
comparing values from different systems and eventualy to run integrationtests and see if the results are as expected and if not to easily find any difference. The comparer is based upon newtonsoft.json. It was developed for comparing results of a migration project.

This is a fork of https://github.com/Patrickkk/Json.Comparer, whom I give all the credit to. 
The lib doesn't look like it has been updated in a while, so I forked it and added 
multiple targets and replace the .bat file build system with Cake.

## Features
- Compare json files and return difference.

To build this utlity, follow the directions below:
### Build 
1. Start Powershell.
2. type `git clone https://github.com/rvegajr/Json.Comparer`
3. navigate to `Json.Comparer`
4. Type `.\build.ps1` and hit enter

## Built With

* [Visual Studio 2017](http://www.visualstudio.com) - The development environment used
* [Cake](https://cakebuild.net) - The build and deployment framework used

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Patrickkk** - *Initial work* - [Patrickkk](https://github.com/Patrickkk/Json.Comparer)
* **Ricky Vega** - *Multi-targeting and cake build* - [Noctusoft](https://github.com/rvegajr)

## License
Please read [LICENSE](https://github.com/Patrickkk/Json.Comparer/blob/master/LICENSE)