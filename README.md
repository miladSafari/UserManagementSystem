 سلام و عرض ادب خدمت شما عزیزان

پروژه را تکمیل و تست نمودم در هر دو حالت Rest API  و gRPC ، متدها را فراخوانی کردم و خروجی دریافت کردم.
لازم به ذکر است که من در Rest API تسلط کاملی دارم ولی اولین بار در پروژه ای از gRPC استفاده کردم که جالب توجه بود . برای درک خود gRPC از لینکی که خدمتتان ارسال می کنم استفاده کردم و کمی هم برای نوشتن کلاس UserService.proto و UserGrpcService از هوش مصنوعی deepseek استفاده نمودم ولی هر بخش را که استفاده می کردم ، مطالعه می کردم و قوانینش را یاد میگیرفتم.

از معماری Onion Architecture و رویکرد DDD, TDD در پروژه استفاده کردم . تست نویسی بر روی دامنه ی بیزینس موجب صرفه جویی در چک و تیک کردن هایی که در لایه های مختلف باید انجام بگیرد ، در خود پروژه ی تست بررسی می گردد.
لاگ ها در مسیر C:\Users\MrMiladSafari\Desktop\Rira\UserManagementSystem\UserManagementService.gRPCService\bin\Debug\net10.0\logs قرار دارد به طوری که خطاها با تاریخ مشخص در این مسیر نوشته میشود.

لینک هایی که برای انجام پروژه استفاده کردم ، به شرح زیر است :

https://stackoverflow.com/questions/11696060/how-to-validate-datetime-format

https://www.c-sharpcorner.com/article/crud-operation-and-microservice-communication-using-grpc-in-net-core-6-web-api?srsltid=AYCX9vJQRVidipTFt7R3as2wWqoZpkJ91qnycUQv2J3Tf8rhC7gB0nqs

https://stackoverflow.com/questions/19942395/pagedlist-using-linq-skip-and-take-but-show-paging-using-count-of-results

https://stackoverflow.com/questions/20185015/how-to-write-log-file-in-c

https://stackoverflow.com/questions/63899398/how-do-i-convert-from-google-protobuf-wellknowntypes-timestamp-to-system-datet

و قطعه کد زیر:
<ItemGroup>
  <Protobuf Include="Protos\UserService.proto"
            GrpcServices="Both" />
</ItemGroup>

راهنمایی برای اجرای پروژه :
1- پروژه را روی https (Kestrel) اجرا کرده و در browser ، لینک زیر را جایگذاری می کنیم:
https://localhost:5001/swagger/index.html
