module numbers

open Giraffe.HttpHandlers
open Giraffe.Middleware
open Giraffe.Razor.HttpHandlers
open Giraffe.Razor.Middleware
open HelloGiraffe.Models
open Hopac
open HttpFs.Client

let getdata() =
      Request.createUrl Get "http://www.google.com"
      |> Request.responseAsString
      |> run


let createRoutes() = 
    choose [
        GET >=>
            choose [
                route "/" >=> razorHtmlView "Index" { Text = getdata() }
                route "/other" >=> razorHtmlView "Index" { Text = "Some other page" }
            ]
        setStatusCode 404 >=> text "Not Found" ]