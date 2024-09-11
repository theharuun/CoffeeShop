# CoffeeShop
## Açıklama
CoffeeShop, bir kafe yönetim sistemi için geliştirilmiş bir uygulamadır. Bu proje, siparişleri ve kahve çeşitlerini yönetmek için tasarlanmıştır. Kullanıcılar, siparişler oluşturabilir, mevcut siparişleri güncelleyebilir ve silebilirler. Ayrıca, çeşitli kahve türleri ekleyebilir ve güncelleyebilirsiniz.

## Özellikler
Sipariş Yönetimi: Yeni siparişler oluşturma, mevcut siparişleri güncelleme ve silme.
Kahve Yönetimi: Kahve türlerini ekleme, güncelleme ve silme.
Çoklu Kahve Türleri: Tek bir siparişte birden fazla kahve türü seçme.
Kahve Türleri Güncelleme: Mevcut kahve türlerinin sadece belirli alanlarını güncelleme.
Doğrulama: Siparişler ve kahve türleri için giriş doğrulama işlemleri.
## Kurulum
### Gereksinimler
.NET 7.0 SDK
Visual Studio 2022 veya daha yenisi
Postman (API testleri için isteğe bağlı)
Adımlar
Bu depo bağlantısını kullanarak projeyi klonlayın: "git clone https://github.com/theharuun/CoffeeShop.git"

Proje dizinine gidin: cd CoffeeShop

Gerekli NuGet paketlerini yükleyin: dotnet restore

Projeyi çalıştırın:dotnet run


## Kullanım
Projeyi çalıştırdıktan sonra, API'yi Postman gibi bir araçla test edebilirsiniz. API uç noktaları aşağıda listelenmiştir:

## Siparişler
GET /api/orders: Tüm siparişleri alır.
GET /api/orders/{id}: Belirli bir siparişi alır.
POST /api/orders: Yeni bir sipariş oluşturur.
PUT /api/orders/{id}: Belirli bir siparişi günceller.
DELETE /api/orders/{id}: Belirli bir siparişi siler.

## Kahve Türleri
GET /api/coffees: Tüm kahve türlerini alır.
GET /api/coffees/{id}: Belirli bir kahve türünü alır.
POST /api/coffees: Yeni bir kahve türü ekler.
PUT /api/coffees/{id}: Belirli bir kahve türünü günceller.
DELETE /api/coffees/{id}: Belirli bir kahve türünü siler.

## Testler
Örnek amaçlı sadece coffee operations oluşturulmuştur bencer yöntemler ve benzer kod birikimi olucağından kendimi geliştirmek için çalıştığım bu projede tekrar kodlarını tekrar yazılmamıştır.

## Katkıda Bulunma
Katkıda bulunmak isterseniz, lütfen şu adımları izleyin:
Bu depoyu çatallayın.
Yeni bir dal oluşturun (git checkout -b feature-xyz).
Değişikliklerinizi yapın ve test edin.
Dalınızı push edin (git push origin feature-xyz).
Bir çekme isteği oluşturun.

##Lisans
Bu proje MIT Lisansı altında lisanslanmıştır.

