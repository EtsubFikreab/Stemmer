# Amharic Stemmer & Search Engine

A comprehensive Amharic text processing toolkit and search engine built with C# and ASP.NET Core. This project implements a rule-based Amharic stemmer and a document retrieval system using the Vector Space Model (VSM) with TF-IDF ranking.

## Features

### 1. Preprocessing Pipeline
- **Lexical Analysis**: 
    - Normalizes common Amharic abbreviations (e.g., `ት/ቤት` -> `ትምህርት ቤት`).
    - Normalizes character variants (e.g., different forms of 'ha', 'hu', etc.).
    - Removes punctuation, special characters, and numbers.
- **Stop Word Removal**: Filters out common Amharic functional words that don't contribute to the semantic meaning of documents.

### 2. Amharic Stemmer
A sophisticated rule-based stemming algorithm that includes:
- **Transliteration**: Converts Amharic characters to a Latin-based representation for easier rule processing.
- **Affix Removal**: Removes common Amharic prefixes and suffixes.
- **Pattern Matching**: Handles morphological variations like `CvCv` and `CvC` patterns.
- **Word Conservation**: Maintains a list of words that should not be stemmed.

### 3. Search Engine & Ranking
- **Database**: Uses SQLite to store documents, their original content, and their stemmed versions.
- **Indexing**: Pre-stems documents upon insertion to optimize search speed.
- **TF-IDF Ranking**: Calculates Term Frequency (TF) and Inverse Document Frequency (IDF) for accurate document weighting.
- **Cosine Similarity**: Ranks search results based on the cosine of the angle between the query vector and document vectors.

## Screenshots

| Search Home | Search Results |
|:---:|:---:|
| ![Search Home](assets/Screenshot%202025-03-26%20220217.png) | ![Search Results](assets/Screenshot%202025-03-26%20220238.png) |

| Add Document | References |
|:---:|:---:|
| ![Add Document](assets/Screenshot%202025-03-26%20220247.png) | ![References](assets/Screenshot%202025-03-26%20220306.png) |

## Project Structure

- `Stemmer/preprocessing/`: Contains the core logic for text processing.
    - `LexicalAnalyzer.cs`: Abbreviation normalization and cleaning.
    - `StopWordRemover.cs`: Stop word filtering.
    - `stemmer.cs`: The core Amharic stemming algorithm.
    - `ranking.cs`: VSM and TF-IDF implementation.
    - `DBProcess.cs`: SQLite database interactions.
- `Stemmer/Pages/`: Razor Pages for the web interface (Search, Add Document, Results).
- `Stemmer/Database/`: SQLite database file.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later.
- SQLite.

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/EtsubFikreab/Stemmer.git
   ```
2. Navigate to the project directory:
   ```bash
   cd Stemmer
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```

### Usage
1. Run the application:
   ```bash
   dotnet run --project Stemmer
   ```
2. Open your browser and navigate to `http://localhost:5000` (or the port specified in the output).
3. **Add Documents**: Use the "Add Document" page to input Amharic text into the system.
4. **Search**: Enter an Amharic query on the home page to see ranked results.

## Technologies Used
- **C# / .NET Core**: Back-end logic and web framework.
- **Razor Pages**: Front-end templating.
- **SQLite**: Lightweight database for document storage.
- **Regex**: Advanced text pattern matching for lexical analysis.
