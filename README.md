# BankApp / English
The Bank App that targets the go through to learn how tos


# BankApp / Turkish
Bu uygulama SOA dersi için geliştirilmektedir ve aşağıdaki senaryo üzerinden geliştirilecektir.

## “SOA” BANKA SENARYOSU
	Merhabalar HFTTF Yazılım,

	X Bankası olarak sizlerle bankamıza ait İnternet Bankacılığı Uygulamamız’ı geliştirmenizi istemekteyiz ve birazdan belirteceğimiz gereksinimlere uygun olarak sizlerden geliştirme teklifi yapmanızı beklemekteyiz. 
	
	Sizlerden bankamızın ticari kimliğini korumanız şartıyla aşağıdaki isterleri istemekteyiz.

Müşteriler
	Müşteriler bildiğiniz gibi bir bankanın tüm değeridir. Müşterilerimiz sistemimize kolayca kayıt olabilir, kaydını silebilir, döviz birimini kendisi bir defaya mahsus seçebilir ve bankacılık işlemleri yapabilir.
	Bir müşterinin bilgileri, Müşteri Numarası, Adı-Soyadı, Şifresi, Bakiyesi, Bakiye Tipi bulunmaktadır.

İşlemler
	Müşterilerimiz İnternet Bankacılığı üzerinden havale işlemlerini, en yakın ATM’den doğrulama yaparak kartsız olarak hesabından para çekme işlemi, para yatırma işlemi yapabilmelidir.
	Bir işlem içerisinde bulunması gereken, İşlem Tarihi, Miktarı, İşlem Yapan Müşteri Numarası, varsa Alıcı Müşteri Numarası ve İşlemin Başarı Durumu. 

	Sizlerden ayrıca ilgili uygulamanın gerekli mühendislik şartlarına uygun olması yani, test, güvenlik, geliştirilebilirlik, hata raporları, esneklik ve kesinlikle tutarlılık konusunda  başarılı olması beklenmektedir. Bu kriterler CMMI standartlarında olması istenmektedir.

	Yukarıdaki gereksinimlere göre projeye yönelik teklifinizi beklemekteyiz.

İyi çalışmalar ve günler dileriz,

En içten saygılarımla,

X BANK Ürün Satın Alma Müdürü


--------------------------------
# BankApp Usages
------
1.  Clone this project and execute the DbSql in your Windows Authenticated MSSQL Database.
2.  When you have database name XBankDB you are ready to go.
3.  BankApp solution doesn't have any startup project type this is why you have to Build it manually for .dlls. Build -> Build Project
4.  Once you have the .dlls You need to import them into the projects.
	- For BankApp-WebService needed dlls: Bank.BusinessLogic, Bank.Models, Bank.Commons
	- For BankAppWeb.Pure & BankAppForm.Pure needed dlls: Bank.BusinessLogic, Bank.Models, Bank.Commons,
	- For BankAppWeb.Soap & BankAppForm.Soap needed dlls: Bank.Models and Bank Commons,
	- For BankAppWeb.RestApi & BankAppForm.RestApi needed dlls: Bank.Models and Bank.Commons,
	- For BankAppWebApi needed dlls: Bank.BusinessLogic, Bank.Commons, Bank.Models
To add these dlls in the project righ click on the project in the solution explorer and Add -> Reference and Browse the .dlls location.
5. When you finished the adding dependencies and want to use Soap and RestApi projects in Web and Form you need to start and update the "PORT" informations in the source code.
	- For SOAP projects update the Connected Services with your debugged service urls/wsdls.
	- For REST projects update the port informations in source code.
6. Then you are ready to go.
If you have any kind of problems when you are setting up the projects you can text me anywhere you can.

# BankApp Kullanımı
-------------------
1. Projeyi klonlayın ve DbSql içerisinde bulunan scripti Windows Authenticated bir MSSQL Veritabanında çalıştırınız.
2. XBankDB adında bir veritabanına saihp olduysanız devam etmeye hazırsınızdır.
3. BankApp çözümü içerisinde bir başlatılabilir proje tipi barındırmadığı için gerekli .dll'leri elde etmek için el ile Build almak durumundayız. Bunun için Build -> Build Project ile işlemi tamamlayınız.
4. .dll'leri elde ettikten sonra bu dosyaları projelerimize dahil etmeliyiz.
	- BankApp-WebService için gerekli dlller: Bank.BusinessLogic, Bank.Models, Bank.Commons
	- BankAppWeb.Pure & BankAppForm.Pure için gerekli dlller: Bank.BusinessLogic, Bank.Models, Bank.Commons,
	- BankAppWeb.Soap & BankAppForm.Soap için gerekli dlller: Bank.Models and Bank Commons,
	- BankAppWeb.RestApi & BankAppForm.RestApi için gerekli dlller: Bank.Models and Bank.Commons,
	- BankAppWebApi için gerekli dlller: Bank.BusinessLogic, Bank.Commons, Bank.Models
Bu .dll'leri projelerinize eklemek için "Çözüm Gezgini" üzerinden projeye sağ tıklayıp Ekle -> Referans seçeneğini seçip çıkan pencerede Bul/Ara ile .dll'lerimizin bulunduğu dosyadan gerekli .dll'leri projelere uygun bir şekilde ekleyiniz.
5. Bütün bağlantıları/bağımlılıkları sağladığınızda Web ve Form uygulamarı içerisinde Soap ve Rest uzantılı projeleri kullanmak istiyorsanız. BankApp-WebService ve BankAppWebApi uygulamarını başlatmanız gerekmektedir ve bu başlatımlardan sonra "PORT" bilgilerini kaynak kodlarda düzenlemeniz gerekiyor.
	- SOAP projeleri için "Uzak/Bağlı Servisler"'inizi projeye eklemeniz ve url/wsdl bilgilerini güncellemeniz gerekmektedir.
	- REST projeleri için ise kod içerisinde bulunan PORT'ları güncellemeniz gerekmektedir.
6. Tüm bunları yaptıysanız her şey tamam demektir.
Kurumlumla ilgili sorunlarınız için herhangi bir sosyal hesabımdan bana ulaşabilirsiniz.
