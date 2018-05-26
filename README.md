# dotnet healthcheck

## Installation

`dotnet install tool -g healthcheck'

## Usage

`dotnet healthcheck {url}`

### What it does

* Performs an HTTP GET request to the provided url.
* Logs Alive and exits with code 0 if the response's status code = 200 (OK).
* Logs Dead and exits with code 1 if the response's status code is not 200 (OK)
