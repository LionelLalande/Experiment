module DDD_CQRS_ES_FSharp.ProductContext

    open System

    type Id = Guid

    type Command = 
        | Create of name: string
        | Rename of name: string

    type Event =
        | Created of id: Id * name: string 
        | Renamed of name: string

    type Product = { Id: Id; Name: string }

    let apply (product: Product) = 
        function
        | Created (id, name) -> { Id = id; Name = name }
        | Renamed name -> { product with Name = name }

    module private Assert =
        let isValidName name = if String.IsNullOrEmpty(name) then invalidArg "name" "The name must not be null." else name
        
    let createNew name =
        (Guid.NewGuid(), name)
    
    let exec (product: Product) =
        // for c# devs: inner function definition!
        let applyCore (event: Event) =
            let newProduct = apply product event
            newProduct
        // for c# devs: function definition!
        function
        | Create(name) -> name |> Assert.isValidName |> createNew |> Created |> applyCore
        | Rename(name) -> name |> Assert.isValidName |> Renamed |> applyCore
    