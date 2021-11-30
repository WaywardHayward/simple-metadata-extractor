# simple-metadata-extractor
A console application which can extract metadata from image files and save it to json

## Getting Started

Clone the code to a local directory using the command:

    git clone https://github.com/WaywardHayward/simple-metadata-extractor.git

Then to Run this console app you need to run the following commands from the root of the project:

1. Resolve dependencies

        dotnet restore

2. Run the application

        dotnet run 

3. The application will then prompt for a file or directory path.

    When you enter a path to a file or directory, the application will extract metadata from the file or directory and save it to a json file in the same directory.

## Limitations

This application can only extract metadata from image files.

