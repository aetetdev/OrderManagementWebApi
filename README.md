# OrderManagementWebApi

Basit bir e-ticaret sipariþ yönetim API'si. Bu repo SQLite + EF Core ile çalýþýr ve aþaðýdaki temel iþlemleri saðlar:

- Ürün ekleme / listeleme / getirme
- Yeni sipariþ oluþturma (stok kontrolü ile)
- Kullanýcýnýn tüm sipariþlerini listeleme
- Sipariþ detayýný getirme
- Sipariþ silme

## Tech stack

- .NET8
- ASP.NET Core Web API
- Entity Framework Core8
- Microsoft.EntityFrameworkCore.Sqlite (SQLite provider)
- Swagger (Swashbuckle) — otomatik API dökümantasyonu

## Kurulum & Çalýþtýrma

1. Gereksinimler:
 - .NET8 SDK

2. Projeyi klonlayýn ve dizine gidin:

 ```bash
 git clone <repo-url>
 cd OrderManagementWebApi
 ```

3. Baðýmlýlýklarý geri yükleyin ve uygulamayý çalýþtýrýn:

 ```bash
 dotnet restore
 dotnet run
 ```

4. Swagger UI (API test arayüzü) çalýþýrken þu adreste olur:
 `https://localhost:{port}/swagger`

## Database (SQLite)

- Varsayýlan connection string `appsettings.json` içinde `Data Source=OrderManagement.db` þeklindedir.
- Uygulama Development ortamýnda çalýþýrken `EnsureCreated()` ile database otomatik oluþturulur ve seed verisi eklenir.
- Eðer migration kullanmak isterseniz (dotnet-ef yüklüyse):
 ```bash
 dotnet ef migrations add InitialCreate
 dotnet ef database update
 ```
- Hýzlý sýfýrlama: `OrderManagement.db` dosyasýný silip uygulamayý yeniden baþlatýn; uygulama DB'yi yeniden oluþturup seed ekleyecektir.

## Seed edilmiþ test ürünleri

Uygulama ilk çalýþmada aþaðýdaki ürünleri ekler (development):

- Product A
 - Id: `1`
 - Stock:10
 - Price:9.99

- Product B
 - Id: `2`
 - Stock:5
 - Price:19.50

Bu `productId` deðerlerini sipariþ testi yaparken kullanabilirsiniz.

## API Endpoints

Tüm endpoint'ler base path: `/api`

- Products
 - `GET /api/Products` — tüm ürünleri listeler
 - `GET /api/Products/{id}` — tek ürün getirir (id: integer)
 - `POST /api/Products` — yeni ürün oluþturur
 - Body (JSON):
 ```json
 {
	 "name": "Yeni Ürün",
	 "stock":10,
	 "price":12.5
 }
 ```
 - Response: `201 Created` ve oluþturulan ürün objesi (id dahil, integer)

- Orders
 - `POST /api/Orders` — yeni sipariþ oluþturur
 - Body (JSON):
 ```json
 {
	"userId":1,
	"items": [
		{ 
			"productId":1, 
			"quantity":2 
		}
	]
 }
 ```
 - Davranýþ:
 - Her item için ürün bulunur ve `stock` kontrol edilir.
 - Eðer bir ürün bulunamazsa veya stok yetersizse `400 Bad Request` döner `{ "error": "..." }` þeklinde.
 - Stok yeterliyse ürün stoklarý güncellenir ve sipariþ veritabanýna kaydedilir. `201 Created` döner.

 - `GET /api/Orders/user/{userId}` — verilen kullanýcýnýn tüm sipariþlerini döner (userId: integer)
 - `GET /api/Orders/{id}` — sipariþ detayýný döner (items, total vb.) (id: integer)
 - `DELETE /api/Orders/{id}` — sipariþi siler (No Content /204)

## Örnek akýþ (test için)

1. Yeni ürün ekleyin (veya seedli productId kullanýn):
 - `POST /api/Products` ile ürün oluþturun.
2. Sipariþ oluþturun:
 - `POST /api/Orders` body içinde daha önce aldýðýnýz `productId` kullanýn (integer).
3. Sipariþ listesini alýn:
 - `GET /api/Orders/user/{userId}` (ör. userId =1)
4. Sipariþ detayýna bakýn:
 - `GET /api/Orders/{orderId}` (orderId integer)
5. Sipariþi silin:
 - `DELETE /api/Orders/{orderId}`

Örnek HTTP istekleri için proje kökünde `OrderManagementWebApi.http` dosyasý bulunmaktadýr; VSCode REST Client veya benzeri bir araçla çalýþtýrabilirsiniz.

## Hatalar ve dönüþler (özet)

-400 Bad Request
 - `{"error":"{id} Numaralý ürün bulunamadý"}` — gönderilen productId veritabanýnda yoksa
 - `{"error":"{name} isimli üründen {stock} adet var {quantity} sipariþ verilemez"}` — istenen miktar stoktan büyükse
-404 Not Found
 - Kaynak yoksa (ör. GET /api/Products/{id} için geçersiz id)
-201 Created
 - Baþarýlý oluþturma (Product veya Order)
-204 No Content
 - Baþarýlý silme
