# SimpleSelenium
SimpleSelenium provides an extensible, domain-specific language for writing browser-based tests.

## Why SimpleSelenium?
Testing browser-based applications generally involves a few basic actions such as opening a browser, navigating to a url, clicking a link, etc. Many testing frameworks express these basic actions through code. However, writing code for tests has some drawbacks.
* Test creators must have coding skills in the test framework and the language in which the framework is written
* Tests frequently contain a lot of code which often becomes redundant
* Writing code can become tedious and time consuming
* The more test code that is written the greater the risk because code that is written to test other code is often not tested itself

SimpleSelenium avoids these issues by abstracting the basic actions required for browser-based testing into a domain-specific language. Armed with about a dozen commands and an Excel spreadsheet, a tester can quickly document the steps required to perform a test. The only code required is a simple test method containing the assertions for the test. 

SimpleSelenium will soon include assertions as part of the domain-specific language so that test creators will be able to specify assertions in addition to test steps. Future development will include a GUI to assist with managing and writing tests. Support for driving multiple browsers in the same test is also planned. Additionally, a structure for setting up and working with test data will be added in a future version.

## Getting Started


### Test Files
Tests are stored in tab delimited files. Each record in the file contains a record type field and a test identifier field followed by other fields specific to the record type. The first record is a header that provides information about the test and file version. The second record sets up a view which specifies the type of browser in which to run the test. Subsequent records contain the commands required to execute the test steps.

### Header
The header record contains the following columns:
* Record Type - always 'header'
* Test Name - a name that uniquely identifies the test
* Blank - always empty
* File Version - a numeric file version (currently always 1)
* File Date - a date field (for future use by the GUI)
* Blank - always 'blank1'
* Blank - always 'blank2'

### Views
The view record specifies the type of browser in which the test will be performed. Currently SimpleSelenium supports one browser per test, but a future release of will allow multiple browsers to be used within a test. Views are given a unique name so that they can be cached, selected for use, and shared between tests.

Each browser record contains the following fields:
* Record Type - always 'view'
* Test Name - a name that uniquely identifies the test
* View Name - a name that uniquely identifies the view
* Browser Code - code for the type of browser to drive IE: 1, Firefox: 2, Chrome: 4
* Blank - always empty
* Blank - always empty
* Blank - always empty

### Commands
Commands tell SimpleSelenium what actions to take in the browser. The available commands are detailed in subsections below.

Each command record contains the following fields:
* Record Type - always 'command'
* Test Name - a name that uniquely identifies the test
* Command Name - the name of the command to execute
* Blank - always empty
* Target - the element or object upon which the command will operate
* Parameter 1 - additional parameter (as needed)
* Parameter 2 - additional parameter (as needed)

For commands where the Target field represents an html element, the field can be identified by element id or by element name. SimpleSelenium interprets the identifier in the Target field as an element id unless the identifier is enclosed in braces, in which case, the identifier is interpreted as an element name.

Example:
* Element Id: myElementId
* Element Name: {myElementName}

For commands where the Target field represents a browser window, the browser window can be identified by ordinal position or by window name. Numeric Target field values are interpreted as window ordinal positions, and non-numeric values are interpreted as window names. SimpleSelenium provides two constant window identifiers that can be used as window ordinal values:
* MAIN_WINDOW: the main browser window
* LAST_WINDOW: the last browser window that was opened

#### BrowseTo
Navigates the browser to the URL specified in the Target field.

#### Click
Clicks the element specified in the Target field.

#### CloseWindow
Closes the window specified in the Target field.

#### DblClick
Double-clicks the element specified in the Target field.

#### ExecuteScript
Executes the javascript contained in the Target field.

If you are concerned about including javascript code in the test file or have a large script, see the Extending Simple Selenium section of this document for an example of creating a custom command to execute parameterized javascript.

#### ExitFrame
Exits the current iframe element and returns to the containing window. No target is required.

#### FindFrame
Finds and enters the iframe element specified in the Target field.

#### FindWindow
Finds and switches to the window specified in the Target field. A window count can be specified in the Parameter 1 field so that SimpleSelenium will wait until the number of expected windows is present before searching for the specified window.

#### RunMacro
Runs the macro specified in the Target field. For more information about macros see the Macros section of this document.

#### SendKeys
Sends the keystrokes specified in the first Parameter field to the element specified in the Target field. The element is cleared by default before keystrokes are sent. To suppress this behavior, set the second Parameter field to a value that will parse to a boolean false value.

#### Sleep
Sleeps for the number of milliseconds specified in the Target field.

Note: SimpleSelenium automatically detects when a page is ready or an element is available. In most cases, it is not necessary to include sleep commands to control timing in tests.

#### TabOut
Behaves like the SendKeys command except it tabs out of the selected element after sending keystrokes. The element is specified in the Target field. The first Parameter field contains the keystrokes to be sent. If that field is empty, no keystrokes are sent. The element will be cleared before keystrokes are sent unless the second Parameter field contains a value that will parse to a boolean false value.

## Macros
Macros contain sequences of commands that can be reused in any test. For example, the steps to open a web application and log in could be placed in a macro because those steps are not specific to a single test and will most likely be used in numerous test classes for a given web application.

Macros contain a header as the first record and commands in all subsequent records. The header structure is:
* Record Type - always 'header'
* Macro Name - a name that uniquely identifies the macro
* Blank - always empty
* File Version - a numeric file version (currently always 1)
* File Date - a date field (for future use by the GUI)
* Blank - always empty
* Blank - always empty

## Adding Tests to Visual Studio
SimpleSelenium works by convention to locate tests and macros which reduces the amount of code required to implement a test. To take advantage of the built in conventions, test and macro files must be named appropriately, and the test method names must the same as the test file name. For cases where a convention is not desirable, SimpleSelenium provides a way to override each of the conventions.

### Test and Macro Files
To setup your Visual Studio test project to follow convention, add the following directory structure to your test project. Save your macro files into the Macros directory and your test files into the Tests directory. Test files should be named the same as the test name in the second column of the file and be given a .txt extension. Macro files should be named the same as the macro name in the second column of the file and be given a .txt extenstion.
```
TestConfig
 |-- Macros
 |-- Tests
```
The convention for locating tests and macros and for setting file extensions can be overriden in the App.config file. The configuration snippet below shows the  settings that can be added to appSettings in order to override the conventions.

```xml
  <appSettings>
    <add key="TestFileDir" value="[your custom test directory]" />
    <add key="TestFileExtension" value="[your custom test file extension]" />
    <add key="MacroFileDir" value="[your custom macro file directory]" />
    <add key="MacroFileExtension" value="[your custom macro file extension]" />
  </appSettings>
```
### Creating a Test Method
[Under Construction]

### Caching Viewers
[Under Construction]

### Setup and Teardown
[Under Construction]

## Extending SimpleSelenium
[Under Construction]

## Miscellaneous Information
[Under Construction]

Talk about these topics somewhere in the document:
Convention is one test per file with the test name, file name, and test method name being the same.
Add info about window count to all commands that support it.
Logging

