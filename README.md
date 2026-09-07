# Geometry

A geometry library for the AEC sector.

## Motivations

This geometry kernel is a part of a couple projects that I am undertaking concurrently to this endeavour. One is an MArchProf thesis (starting 2027). The other is yet-to-see-the-light-of-day... :eyes:

Now I am **very** aware of the Dunning-Kruger effect when making this statement and am happy to be wrong:

**As far as I know, no geometry kernel exists for the AEC that matches the semantics I am trying to achieve (see below).**

### My requirements:

- I want a core geometry kernel that defines the mathematical properties and relationships between abstract and simple geometry types. This library should be extensible to add more types as I see fit, but should have the core principle that it is abstracted away from the world of AEC and all the dogma that comes with this industry.
- I want an abstract "entities" layer, which uses the core geometry kernel to start to define AEC-related entities; this may be a beam, a wall, a floor, etc.
- I want to capture the types defined in the core geometry kernel to be defined as [JsonSchema](https://json-schema.org/). This is for a number of reasons.
- I want to be able to serialize and deserialize these two layers and their types to JSON as well as other file formats. For now, JSON is fine.
- I want to be able to convert these two layers and their types to other SDK types, such as Speckle or IFC. Speckle is fine for now (I don't like IFC).
- I want all of this to live outside of an proprietary CAD/BIM software.
- I want this to be open-source, for the *greater good*.
- I want the architecture of the library to be extensible and comprehensible.

As far as I know, something with all of these requirements does not exist. **<u>Please tell me if I am wrong.</u>**

## The semantics

### Core Geometry Layer

To me, there is *pure* geometry; the kind we get taught in school. The kind that Euclid talked about, and that we have mathematical equations for. A vector, a line, a BRep, a mesh, the list goes on.

This kind of geometry lives in a world free of the dogma of AEC. It could be used in graphics, a computation, a query, and is abstract. It uses equations and properties to define a piece of geometry; it doesn't have metadata.

The geometry layer is mutable - a lot of these types will be value types (.NET structs), and so will be mutable by nature.

I intend on building an immutable, composable layer over top of these types as a way to have composable operations (for those Grasshopper lovers out there).

### Descriptors a.k.a metadata

#### Why not call it metadata?

To get away from the dogma of the name *metadata*. Everyone has opinions on what *metadata* is.

#### What are descriptors?

Descriptors are used to describe an Entity. This could be its name, its description, an Id, a set of geometry, a colour, a product sku, the list goes on. They are the things that if you saw this entity on the street, you'd use them to describe **it**.

### Entities

Finally, we have entities.

Entities are a combination of *descriptors*. An entity may have a name, a colour, an Id, a list of geometry, the list goes on. Essentially, it is a collection of descriptors.

An entity is <u>**not**</u> geometry. I know that can be a hard thing to grasp. Geometry is an an abstract concept that doesn't *exist*. Geometry is merely a descriptor of an entity.

An entity's display value can render on a screen as a mesh, sure, but that would come through determining if an entity has the quality *is renderable* (in .NET, that could be IRenderable) and then querying the underlying *display value* a.k.a, a combination of the entity's material and geometry (converted to a mesh, of course). 

Simply put: an entity is a thing where one of its qualities is a list of abstract geometries; it is **not** attributed geometry.

### Overview on semantics

Here is a little diagram to explain the semantics as they're in my head

``` mermaid
flowchart LR

A[Geometry]
B[Name]
C[Colour]
D[Supplier]
E[Embodied Co2]
G[...]
F[Beam]
subgraph Descriptors
A
B
C
D
E
G
end

Descriptors --> F
```

## Library Structure

The library is structured as such:

- Core: A pure, unbiased, undogmatic abstract mathematical geometry layer
- Entities.Core: A home for the abstractions that define entities.
- Entities.%: A layer where entities start to be described as a set of descriptors. The *%* represents a particular sub-sector of AEC. This could be "Structural" for structural engineering, or "Sustainability" for sustainability. The list goes on. Feel free to add it.
- Core.Schemas: A collection of schemas that have been defined for the types as defined in **Core**. If you are defining your own types at any point, you are encouraged to publish these schemas. Please see %schema-library-link-goes-here% for info on **Schemas**.
- *.Speckle: Converters for converting to and from Speckle types. Speckle is awesome. You should try it out if you haven't already.

## Finally...

This is meant to be fun.

Am I insane? Maybe.
Will I learn a sh*t tonne on the way. Absolutely. 
Will this project, die a slow, painful death? Only time will tell!

I know this is a big undertaking. This is a small-ish part to play in a bigger vision for the AEC industry.

More to come on that later...

