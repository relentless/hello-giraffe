module numbers

//open Microsoft.AspNetCore.Builder
//open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
//open Microsoft.Extensions.Logging
//open Microsoft.Extensions.DependencyInjection
open Giraffe.Tasks
open Giraffe.HttpHandlers
open Giraffe.Middleware
open Giraffe.Razor.HttpHandlers
open Giraffe.Razor.Middleware
open Giraffe.HttpContextExtensions
open HelloGiraffe.Models
open ServiceInterfaces

//let myService = TransportService()

let detailsView (service: ITransportService) (next : HttpFunc) (ctx : HttpContext) = 
    task {
        return! razorHtmlView "Index" { Text = service.GetData() } next ctx
    }

let withTransportService() =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let transportService =  ctx.GetService<ITransportService>()// ctx.GetService<ITransportService>()// (ctx.RequestServices.GetService(typeof<ITransportService>) :?> ITransportService)
            return! (detailsView transportService) next ctx
        }

let createRoutes() = 
    choose [
        GET >=>
            choose [
                route "/" >=> withTransportService()
                route "/other" >=> razorHtmlView "Index" { Text = "Some other page" }
            ]
        setStatusCode 404 >=> text "Not Found" ]