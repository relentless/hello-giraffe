module numbers

open Giraffe.HttpHandlers
open Giraffe.Middleware
open Giraffe.Razor.HttpHandlers
open Giraffe.Razor.Middleware
open HelloGiraffe.Models
open ServiceInterfaces

let myService = TransportService()

let createRoutes() = 
    choose [
        GET >=>
            choose [
                route "/" >=> razorHtmlView "Index" { Text = (myService :> ITransportService).GetData() }
                route "/other" >=> razorHtmlView "Index" { Text = "Some other page" }
            ]
        setStatusCode 404 >=> text "Not Found" ]