namespace ServiceInterfaces

open Hopac
open HttpFs.Client

type TransportService () =
   interface ITransportService with
      member x.GetData() = 
        Request.createUrl Get "http://chargepoints.dft.gov.uk/api/retrieve/status?format=json"
          |> Request.responseAsString
          |> run