# Json.Comparer - Compare JTokens, JObjects, JArrays and 
I have created Json.Comparer in order to allow for easy comparison of any object in .net and 
comparing values from different systems and eventualy to run integrationtests and see if the results are as expected and if not to easily find any difference. The comparer is based upon newtonsoft.json. It was developed for comparing results of a migration project.

This is a fork of [Json.Comparer](https://github.com/Patrickkk/Json.Comparer), whom I give all the credit to. 
The lib doesn't look like it has been updated in a while, so I forked it and added 
multiple targets and replace the .bat file build system with Cake.

## Features
- Compare json files and return difference.
- New compareLogic class allows for easier comparing
- Object utilizes Newton soft self referencing logic so cyclical objects are respected

## Quickstart
Install the nuget package using following command:
``` PS
Install-Package Json.Comparer
```
Use the following for some sample usages:
``` CS
/* Use the following classes for testing*/
    public class ComplexTestObject : ComplexTestItem
    {
        public List<ComplexTestItem> ListofComplexItems { get; set; } = new List<ComplexTestItem>();
        public Dictionary<string, ComplexTestItem> DictionaryofComplexItems { get; set; } = new Dictionary<string, ComplexTestItem>();
        public ComplexTestObject()
        {
            this.ListofComplexItems.Add(new ComplexTestItem() { StringValue = "STR1", DecimalValue = 4.3M, IntValue = 99 });
            this.ListofComplexItems.Add(new ComplexTestItem() { StringValue = "STR2", DecimalValue = 2.6M, IntValue = 14 });
            this.DictionaryofComplexItems.Add("Key 1", new ComplexTestItem() { StringValue = "STR3", DecimalValue = 1213.31M, IntValue = 31231 });
            this.DictionaryofComplexItems.Add("Key 2", new ComplexTestItem() { StringValue = "STR4", DecimalValue = 963.992M, IntValue = 913 });
        }
    }
    public class ComplexTestItem
    {
        public int IntValue { get; set; } = 2;
        public string StringValue { get; set; } = "VALUE";
        public decimal DecimalValue { get; set; } = 1.2M;
    }
    public class ComplexTestItem2 : ComplexTestItem
    {
        public DateTime DateValue { get; set; } = DateTime.Now;
    }

    public void CompareLogicCompareObjectsSame()
    {
        var o1 = new ComplexTestObject();
        var o2 = new ComplexTestObject();
        o2.ListofComplexItems.RemoveAt(1);
        o2.ListofComplexItems.Add(new ComplexTestItem2());

        CompareLogic compareLogic = new CompareLogic();
        ComparisonResults res = compareLogic.Compare(o1, o2);
        if (!res.AreEqual) {
            Console.Writeline("They Are not Equal!!");
        }

        ComparisonResults res1 = (new CompareLogic()).Compare(new ComplexTestObject(), new ComplexTestObject());
        if (res1.AreEqual) {
            Console.Writeline("They Equal!!");
        }

    }    
```
## Build 
To build this utlity, follow the directions below:
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