# webcast.forecast

About DataBase decisions:
- I decided to use redis for this kind of data, considering that i will not need to store a history and will be easy to delete with redis TTL
As a disaster recovery option i used a AOF strategy, considering that our system should have a lot of reads 
but not a lot of writes, i will write the data to disk in every write opperation, this will limit us for roughly 
200 writes/second for a spinning disk, and maybe a few tens of thousands for an SSD, as we dont have a batch opperation
this shouldnt be a problem.
- If i need to store all the history i probably will use a mongodb, since the objective need to scale data reading and dont need ACID transactions
- In my repository i added a strategy to create and update a "consolidade Index" with all actual week indexes, i maintened this rule and logic in infrastructure because this logic is associated with my database


.NET CORE decisions
- I spend some time thinking if my WheateForecast should be a value object because will be immutable(in my technical workflow but not in bussiness conception) and represents a value, but i decided to not because Date will be my identity and valueobjects doesnt have a identity and in the bussiness concept forecast isnt immutable
- I will mantain SummariesAndMaxTemperature on my Domain as a static then i dont store in my heap and GC dont need to concern with this dictionary in every WeatherForecast instance
- Im my controller just have "OK" because in this scenario my application workflows are too simple and i dont have "application errors", so i handle everything in my ExceptionHandlerMiddleware, if it was a case i will probably would use Either<ErorResponseModel, "TypeThatIWant"> and if i receive the ErrorResponse i would return BadRequest on my controller
- I just make tests in my Domain Models because i dont have any application service or domain service with a logic to test if i had, i would probably use moQ framework to mock my interfaces

Improvments:
Add Serilog to structured logs
Add logs
Fill appsettings.json with environment variables(or another place like azureDevops library/keyVault)
Create mappers classes "DomainToDTOMapper, DtoToDomainMapper"
