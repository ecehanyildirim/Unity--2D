using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private Text falseText;
    [SerializeField] private Button answerButton1, answerButton2, answerButton3, answerButton4;
    
    

    List<Question> _questions;

    public Text answerText, question,trueText;    
    public int count=0;
    public float targetTime;

    public bool playAgain = false;
    public bool oyunBitti = false;


    GameManager gameManager;
    PannelManager panelManager;

    private void Awake()
    {
        panelManager = Object.FindObjectOfType<PannelManager>();
        gameManager = Object.FindObjectOfType<GameManager>();
    }
    public QuestionManager()
    {
        _questions = new List<Question>
        {
            new Question{QuestionId=1 , AnswerId=1, Answer1="Büst",Answer2="Peyzaj",Answer3="Fresko",Answer4="Gravür", CorrectAnswer="Fresko", Questions="Yaş sıva üzerine toprak boyalarla yapılan duvar resmine verilen isim, aşağıdakilerden hangisidir?",},
            new Question{QuestionId=2 , AnswerId=2, Answer1="Tunus",Answer2="Fars",Answer3="Mısır",Answer4="Ürdün", CorrectAnswer="Mısır", Questions="Piramitler şu an hangi ülkenin sınırları içerisinde yer almaktadır?",},
            new Question{QuestionId=3 , AnswerId=3, Answer1="Teşkilati Esasiye Kanunu",Answer2="Amasya Genelgesi",Answer3="Sivas Genelgesi",Answer4="Misak-ı Milli", CorrectAnswer="Amasya Genelgesi", Questions="Egemenlik ilkesinden ilk kez nerede bahsedilmiştir?",},
            new Question{QuestionId=4 , AnswerId=4, Answer1="Yıldırım Bayezit",Answer2="II.Mehmet",Answer3="I.Murat",Answer4="II.Murat", CorrectAnswer="II.Murat", Questions="Fatih Sultan Mehmet’in babası kimdir?",},
            new Question{QuestionId=5 , AnswerId=5, Answer1="Paleografi",Answer2="Arkeoloji",Answer3="Epigrafi",Answer4="Kronoloji", CorrectAnswer="Epigrafi", Questions="Taş, metal, tahta, mermer, seramik gibi kalıcı ve sert maddeler üzerine yazılan yazıları inceleyen bilim dalı, aşağıdakilerden hangisidir?",},
            new Question{QuestionId=6 , AnswerId=6, Answer1="Albert Einstein",Answer2="Niels Bohr",Answer3="Max Plack",Answer4="Emmy Noether", CorrectAnswer="Albert Einstein", Questions="Newton mekaniğinin yasalarını değiştiren ve kütle ile enerjinin eşdeğerli olduğunu öne süren sınırlı bağlılık (1905); eğrisel ve sonlu olarak düşünülen dört boyutlu bir evrene ait çekim teorisini veren genel bağlılık (1916); elektro-manyetizma ve yer çekimini aynı alanda birleştiren daha geniş kapsamlı teori denemeleri.Bu teori denemeleri hangi bilim adamına aittir?" ,},
            new Question{QuestionId=7 , AnswerId=7, Answer1="III.Selim",Answer2="II.Mahmut",Answer3="Abdülmecit",Answer4="Abdülaziz", CorrectAnswer="III.Selim", Questions="Osmanlı devletinde imparatorluğun gerileme nedenlerini tespit edip bu gerilemeye karşı alınacak önlemleri belirlemek amacıyla dönemin devlet adamlarına raporlar hazırlatan osmanlı padişahı aşağıdakilerden hangisidir ?",},
            new Question{QuestionId=8 , AnswerId=8, Answer1="Reşat Nuri Güntekin",Answer2="Halide Edip Adıvar",Answer3="Ziya Gökalp",Answer4="Ömer Seyfettin", CorrectAnswer="Halide Edip Adıvar", Questions="Sinekli Bakkal” Romanının Yazarı Aşağıdakilerden Hangisidir?",},
            new Question{QuestionId=9 , AnswerId=9, Answer1="Ahmet Hamdi Tanpınar",Answer2="Orhan Kemal",Answer3="Sait Faik Abasıyanık",Answer4="Yaşar Kemal", CorrectAnswer="Yaşar Kemal", Questions="İnce Memed adlı eseri kim yazmıştır?",},
            new Question{QuestionId=10 , AnswerId=10, Answer1="Frida Kahlo",Answer2="Vincent van Gogh",Answer3="Salvador Dali",Answer4="Pablo Picasso", CorrectAnswer="Salvador Dali", Questions="Belleğin Azmi adlı bu tabloyu hangi ünlü ressam çizmiştir?",},
            new Question{QuestionId=11 , AnswerId=11, Answer1="Kathryn Bigelow",Answer2="Nora Ephron",Answer3="Vera Chytilova",Answer4="Tomris Giritlioğlu", CorrectAnswer="Kathryn Bigelow", Questions="Oscar ödülü alan ilk kadın yönetmen kimdir?",},
            new Question{QuestionId=12 , AnswerId=12, Answer1="Haldun Taner",Answer2="Aziz Nesin",Answer3="Reşat Nuri Güntekin",Answer4="Rıfat Ilgaz", CorrectAnswer="Rıfat Ilgaz", Questions="Hababam Sınıfı kimin eseridir?",},
            new Question{QuestionId=13 , AnswerId=13, Answer1="100",Answer2="10",Answer3="0",Answer4="50", CorrectAnswer="0", Questions="Romen rakamlarında hangi sayı yoktur?",},
            new Question{QuestionId=14 , AnswerId=14, Answer1="Senegal",Answer2="Venezuela",Answer3="Kamboçya",Answer4="Güney Afrika", CorrectAnswer="Güney Afrika", Questions="Hangi ülkenin iki tane başkenti vardır?",},
            new Question{QuestionId=15 , AnswerId=15, Answer1="Ajda Pekkan – Petrol",Answer2="Semiha Yankı – Seninle Bir Dakika",Answer3="Çetin Alp – Opera",Answer4="Nilüfer ve Grup Nazar – Sevince", CorrectAnswer="Semiha Yankı – Seninle Bir Dakika", Questions="Eurovision şarkı yarışmasına 1975 yılında ilk hangi ünlü isim katılmıştır?",},
            new Question{QuestionId=16 , AnswerId=16, Answer1="Esenyurt",Answer2="Küçükçekmece",Answer3="Bağcılar",Answer4="Ümraniye", CorrectAnswer="Esenyurt", Questions="Son verilere göre İstanbul’un en kalabalık ilçesi hangisi?",},
            new Question{QuestionId=17 , AnswerId=17, Answer1="Direnç",Answer2="Amper",Answer3="Voltmetre",Answer4="Ohm", CorrectAnswer="Amper", Questions="Elektrik akımı ölçü birimi hangisidir?",},
            new Question{QuestionId=18 , AnswerId=18, Answer1="Atlantik",Answer2="Hint",Answer3="Arktik",Answer4="Pasifik", CorrectAnswer="Pasifik", Questions="En büyük okyanus hangisidir?",},
            new Question{QuestionId=19 , AnswerId=19, Answer1="Merkür",Answer2="Venus",Answer3="Jüpiter",Answer4="Neptün", CorrectAnswer="Venus", Questions="Aktif yanardağları olan güneş sistemi gezegeni hangisidir?",},
            new Question{QuestionId=20 , AnswerId=20, Answer1="Metan",Answer2="Oksijen",Answer3="Karbondioksit",Answer4="Su Buharı", CorrectAnswer="Oksijen", Questions="Hangisi bir sera gazı değildir?",},
            new Question{QuestionId=21 , AnswerId=21, Answer1="Merkür",Answer2="Venüs",Answer3="Plüton",Answer4="Neptün", CorrectAnswer="Neptün", Questions="Hangi gezegen bir gaz devidir?",},
            new Question{QuestionId=22 , AnswerId=22, Answer1="Io",Answer2="Europa",Answer3="Metis",Answer4="Callisto", CorrectAnswer="Metis", Questions="Hangisi Gallileo'nun keşfettiği Jüpiter uydularından biri değildir?",},
            new Question{QuestionId=23 , AnswerId=23, Answer1="Elektromanyetik",Answer2="Yerçekimsel",Answer3="Zayıf Yerçekimsel",Answer4="Güçlü Nükleer", CorrectAnswer="Zayıf Yerçekimsel", Questions="Hangisi temel bir kuvvet değildir?",},
            new Question{QuestionId=24 , AnswerId=24, Answer1="Lityum",Answer2="Ksenon",Answer3="Argon",Answer4="Helyum", CorrectAnswer="Lityum", Questions="Hangisi bir soygaz değildir?",},
            new Question{QuestionId=25 , AnswerId=25, Answer1="Propan",Answer2="Metanol",Answer3="Doğalgaz",Answer4="Hidrojen", CorrectAnswer="Doğalgaz", Questions="Hangisi fosil yakıttır?",},
            new Question{QuestionId=26 , AnswerId=26, Answer1="Yunanistan",Answer2="Polonya",Answer3="Sırbistan",Answer4="Avusturya-Macaristan", CorrectAnswer="Polonya", Questions="Aşağıdakilerden hangisi I.Dünya Savaşına katılan devletlerden biri olamaz?",},
            new Question{QuestionId=27 , AnswerId=27, Answer1="Fusako Kitashirakawa",Answer2="Nakayama Yoshiko",Answer3="Toshiko Higashikuni",Answer4="Mutsuhito", CorrectAnswer="Mutsuhito", Questions="Japonya'da 'Meji Restarasyonu' denilen reform sürecini başlatan lider kimdir ?",},
            new Question{QuestionId=28 , AnswerId=28, Answer1="Necd Emiri Abdülaziz",Answer2="Hicaz Emiri Şerif Hüseyin",Answer3="Şerif Saad Zaglul",Answer4="Hüseyin Bin Ali", CorrectAnswer="Hicaz Emiri Şerif Hüseyin", Questions="3 Mart 1924 te Halifeliğin kaldırılmasından sonra kendisini Halife ilan eden ve aynı zamanda Arap ülkeleri Kralı olarak bilinen lider kimdir ?",},
            new Question{QuestionId=29 , AnswerId=29, Answer1="N.E.P",Answer2="Leniskovaya",Answer3="S.L.P",Answer4="L.E.P", CorrectAnswer="N.E.P", Questions="Bolşevik ihtilalinden sonra başa geçen Lenin tarafından benimsenen Ekonomi politikası aşağıdakilerden hangisidir?",},
            new Question{QuestionId=30 , AnswerId=30, Answer1="Zeki Velidi Togan",Answer2="Fuat Köprülü",Answer3="Hamdullah Suphi",Answer4="Enver Paşa", CorrectAnswer="Zeki Velidi Togan", Questions="Türkistan Milli Birliğinin kurucusu ve il başkanı olarak bilinen türk lider kimdir ?",},
            new Question{QuestionId=31 , AnswerId=31, Answer1="Vecihi",Answer2="Demirağa",Answer3="Reşit Alan",Answer4="Mehmet Altunbay", CorrectAnswer="Vecihi", Questions="Türkiye'de üretilen ilk uçağın adı nedir?",},
            new Question{QuestionId=32 , AnswerId=32, Answer1="Küçük Kara Balık",Answer2="Küçük Prenses",Answer3="Küçük Prens",Answer4="Kırmızı Balık", CorrectAnswer="Küçük Prens", Questions="Antoine de Saint-Exupery'nin küçükler kadar büyüklerin de okuduğu dünyaca ünlü kitabı nedir?",},
            new Question{QuestionId=33 , AnswerId=33, Answer1="Siyah",Answer2="Mavi",Answer3="Yeşil",Answer4="Kırmızı", CorrectAnswer="Kırmız", Questions="Cumhurbaşkanlığı ve Meclis Başkanlığına ait araç plakalarının zemini hangi renktir?",},
            new Question{QuestionId=34 , AnswerId=34, Answer1="Bulgur",Answer2="Pirinç",Answer3="Mercimek",Answer4="Buğday", CorrectAnswer="Bulgur", Questions="İçli köfte yapımında hangi bakliyat kullanılır?",},
            new Question{QuestionId=35 , AnswerId=35, Answer1="Erzurum",Answer2="Iğdır",Answer3="Kars",Answer4="Van", CorrectAnswer="Iğdır", Questions="Ağrı Dağının Ağrı İli Dışında hangi ilde sınırları vardır?",},
            new Question{QuestionId=36 , AnswerId=36, Answer1="Donmak",Answer2="Yanmak",Answer3="Aç Kalmak",Answer4="Susmak", CorrectAnswer="Yanmak", Questions="Oruç ayına adını veren ramazan sözcüğünün kelime anlamı nedir?",},
            new Question{QuestionId=37 , AnswerId=37, Answer1="Yonca",Answer2="Gelincik",Answer3="Isırgan",Answer4="Madımak", CorrectAnswer="Yonca", Questions="4 yapraklısının şans getirdiğine inanılan bitki türü hangisidir?",},
            new Question{QuestionId=38 , AnswerId=38, Answer1="Ney",Answer2="Zurna",Answer3="Davul",Answer4="Bateri", CorrectAnswer="Davul", Questions="Ramazanda sahur vaktini bildirmek için mahalle aralarında çalınan enstrüman hangisidir?",},
            new Question{QuestionId=39 , AnswerId=39, Answer1="Genelkurmay Başkanlığı",Answer2="Başbakanlık",Answer3="Cumhurbaşkanlığı",Answer4="MİT", CorrectAnswer="Cumhurbaşkanlığı", Questions="Köşk tabiri hangi makam için kullanılır?",},
            new Question{QuestionId=40 , AnswerId=40, Answer1="Kayınçosu",Answer2="Eniştesi",Answer3="Bacanağı",Answer4="Dünürü", CorrectAnswer="Dünürü", Questions="Eşinizin babası sizin babanızın nesi olur?",},
            new Question{QuestionId=41 , AnswerId=41, Answer1="Pokemon",Answer2="Beyblade",Answer3="Yu-gi-oh",Answer4="Bakugan", CorrectAnswer="Pokemon", Questions="Hayali yaratık Pikaçu hangi çizgi filmin kahramanıdır?",},
            new Question{QuestionId=42 , AnswerId=42, Answer1="6",Answer2="7",Answer3="8",Answer4="9", CorrectAnswer="7", Questions="Bir futbol maçında bir takım sahada en kaç kişi olmalıdır?",},
            new Question{QuestionId=43 , AnswerId=43, Answer1="Fazıl Say",Answer2="Erol Sayan",Answer3="Hasan Ferit Alınar",Answer4="Ahmet Adnan Saygun", CorrectAnswer="Erol Sayan", Questions="Sen gözlerimde bir renk kulaklarımda bir ses ve içimde nefes olarak kalacaksın şarkısının bestesi kime aittir?",},
            new Question{QuestionId=44 , AnswerId=44, Answer1="5",Answer2="6",Answer3="7",Answer4="8", CorrectAnswer="7", Questions="Hayvan Adı Olan kaç burç vardır?",},
            new Question{QuestionId=45 , AnswerId=45, Answer1="1",Answer2="2",Answer3="3",Answer4="4", CorrectAnswer="3", Questions="Dini inanışlara göre yeni doğan bir bebeğin adı kulağına kaç kez söylenir?",},
            new Question{QuestionId=46 , AnswerId=46, Answer1="Selvi",Answer2="Sedir",Answer3="Ladin",Answer4="Ardıç", CorrectAnswer="Selvi", Questions="Halk edebiyatımızda uzun boyu anlatmak için kullanılan ağaç hangisidir?",},
            new Question{QuestionId=47 , AnswerId=47, Answer1="Fransa",Answer2="Rusya",Answer3="İrlanda",Answer4="İngiltere", CorrectAnswer="İngiltere", Questions="Hangi ülkenin kraliçesinin eşi prenstir?",},
            new Question{QuestionId=48 , AnswerId=48, Answer1="TUK",Answer2="THY",Answer3="THK",Answer4="TK", CorrectAnswer="TK", Questions="Türk hava yollarının uçuş kodu nedir?",},
            new Question{QuestionId=49 , AnswerId=49, Answer1="Tavşan",Answer2="Sincap",Answer3="Fare",Answer4="Keçi", CorrectAnswer="Fare", Questions="Külkedisi’nin baloya gidebilmesi için, perinin 6 adet ata dönüştürdüğü hayvan hangisidir?",},
            new Question{QuestionId=50 , AnswerId=50, Answer1="2-1",Answer2="5-3",Answer3="6-3",Answer4="5-2", CorrectAnswer="5-3", Questions="Hangi iki sayı tavla oyunun başlangıcında kapı alır?",},
            new Question{QuestionId=51 , AnswerId=51, Answer1="Rihanna",Answer2="Beyoncé",Answer3="Michael Jackson",Answer4="Amy Winehouse", CorrectAnswer="Amy Winehouse", Questions="Back to black adlı albümüyle 5 dalda Grammy ödülü alan ve 2011 yılında kaybettiğimiz ünlü şarkıcı kimdir ?",},
            new Question{QuestionId=52 , AnswerId=52, Answer1="Irak-İran",Answer2="Arabistan-Katar",Answer3="Suriye – Mısır",Answer4="Mısır-Libya", CorrectAnswer="Suriye – Mısır", Questions="Hangi ülke 1958-1961 yılları arasında bir süreliğine birleşerek “Birleşik Arap Cumhuriyeti” ni kurmuştur? ",},
            new Question{QuestionId=53 , AnswerId=53, Answer1="Yüz Akı",Answer2="Alın Teri",Answer3="Şeref Sözü",Answer4="Yüz Kızarıklığı", CorrectAnswer="Yüz Akı", Questions="Saygınlığını yitirmeden bir işin sonlandırılması anlamında kullanılan söz hanesidir?",},
            new Question{QuestionId=54 , AnswerId=54, Answer1="Mavi Çizgi",Answer2="Kırmızı Çizgi",Answer3="Siyah Çizgi",Answer4="Beyaz Çizgi", CorrectAnswer="Kırmızı Çizgi", Questions="Devletlerin, bir konuda taviz vermeyecekleri kurallar hangi sözle ifade edilir?",},
            new Question{QuestionId=55 , AnswerId=55, Answer1="Adana Demirspor",Answer2="Başakşehir",Answer3="Ankaragücü",Answer4="Sivasspor", CorrectAnswer="Ankaragücü", Questions="12 Eylül 1980 darbesinden sonra Kenan Evren’in talimatıyla birinci lige yükseltilen futbol takımı hangisidir?",},
            new Question{QuestionId=56 , AnswerId=56, Answer1="Jean‑Paul Sartre",Answer2="Amin Maalouf",Answer3="Franz Kafka",Answer4="Francine Faure", CorrectAnswer="Amin Maalouf", Questions="Semerkant Yüzüncü Ad ve Çivisi Çıkmış Dünya kitaplarının yazarı kimdir?",},
            new Question{QuestionId=57 , AnswerId=57, Answer1="Doğum",Answer2="Düğün",Answer3="Sünnet",Answer4="18. Yaş Günü", CorrectAnswer="Doğum", Questions="Allah analı babalı büyütsün sözü hangi durumda söylenir?",},
            new Question{QuestionId=58 , AnswerId=58, Answer1="Turuncu",Answer2="Kırmızı",Answer3="Yeşil",Answer4="Sarı", CorrectAnswer="Sarı", Questions="Araçlarda yakıtın bitmek üzere olduğunu gösteren uyarı ışığı genellikle hangi renktir?",},
            new Question{QuestionId=59 , AnswerId=59, Answer1="28",Answer2="7",Answer3="21",Answer4="14", CorrectAnswer="14", Questions="Bir şirkette 1 yıl çalışmış olan bir işçinin yıllık ücretli izni kaç günden az olamaz?",},
            new Question{QuestionId=60 , AnswerId=60, Answer1="Torpil",Answer2="Negatif Ayrımcılık",Answer3="Pozitif ayrımcılık",Answer4="Dışlama", CorrectAnswer="Pozitif ayrımcılık", Questions="Toplumun belirli bir kesimine çeşitli ayrıcalıklar tanıyarak onları desteklemeye ne ad verilir?",},
            new Question{QuestionId=61 , AnswerId=61, Answer1="Orhan Pamuk",Answer2="Jean Paul Sartre",Answer3="Bob Dylan",Answer4="Peter Handke", CorrectAnswer="Jean Paul Sartre", Questions="Yazdıklarının gücünü azaltacağını öne sürerek Nobel ödülünü reddeden, “Bulantı” ve “Duvar” kitaplarının yazarı kimdir?",},
            new Question{QuestionId=62 , AnswerId=62, Answer1="Kütüphane",Answer2="Cami",Answer3="Aşevi",Answer4="Müze", CorrectAnswer="Kütüphane", Questions="Mescid-i Haram’ın yakınlarında bulunan, Hz. Muhammed’in doğduğu ev, bugün ne olarak korunmaktadır?",},
            new Question{QuestionId=63 , AnswerId=63, Answer1="Yeşil",Answer2="Mavi",Answer3="Siyah",Answer4="Kırmızı", CorrectAnswer="Mavi", Questions="Şehir girişlerinde şehrin adı, nüfusu ve rakamının yazılı olduğu tabela ne renktir?",},
            new Question{QuestionId=64 , AnswerId=64, Answer1="Müzik Aleti",Answer2="Hayvan",Answer3="Pörsümüş",Answer4="Ağaç", CorrectAnswer="Müzik Aleti", Questions="Hangisi porsuk kelimesinin anlamlarından biri değildir?",},
            new Question{QuestionId=65 , AnswerId=65, Answer1="İTÜ",Answer2="Cumhurbaşkanlığı",Answer3="ODTÜ",Answer4="TBMM", CorrectAnswer="ODTÜ", Questions="Türkiye’de Nisan 1993’te internet ilk olarak nerede kullanılmaya başlanmıştır?",},
            new Question{QuestionId=66 , AnswerId=66, Answer1="Yolmak",Answer2="Tostlamak",Answer3="Ütülemek",Answer4="Yıkamak", CorrectAnswer="Ütülemek", Questions="Tavuğun tüylerini yakarak yok etme işlemine halk arasında ne ad verilir?",},
            new Question{QuestionId=67 , AnswerId=67, Answer1="Radyo",Answer2="Telsiz",Answer3="Telefon",Answer4="İnternet", CorrectAnswer="Telsiz", Questions="80’li yıllarda arkadaş bulma çağrısı olarak yapılan Arkadaş arıyorum arkadaş ne anonsudur?",},
            new Question{QuestionId=68 , AnswerId=68, Answer1="9",Answer2="3",Answer3="1",Answer4="5", CorrectAnswer="3", Questions="Ambalajın geri kazanabilir bir malzemeden üretildiğini ifade eden geri dönüşüm işaretinde kaç tane ok vardır?",},
            new Question{QuestionId=69 , AnswerId=69, Answer1="Judith Weir",Answer2="Harrison Birtwistle",Answer3="Bertolt Brecht",Answer4="Philip Glass", CorrectAnswer="Bertolt Brecht", Questions="Kafkas Tebeşir Dairesi ve Üç Kuruşluk Opera oyunlarının yazarı kimdir?",},
            new Question{QuestionId=70 , AnswerId=70, Answer1="40",Answer2="25",Answer3="35",Answer4="30", CorrectAnswer="30", Questions="Gündemde olan bedelli askerlik uygulamasında yaş sınırı kaçtır?",},
            new Question{QuestionId=71 , AnswerId=71, Answer1="Yine Bekleriz",Answer2="Size Artık Müsaade",Answer3="Bize Doyum Olmaz",Answer4="Size Doyum Olmaz", CorrectAnswer="Size Doyum Olmaz", Questions="Bir yerden ayrılmak için müsaade istenirken kullanılan söz hangisidir?",},
            new Question{QuestionId=72 , AnswerId=72, Answer1="Çifte Kumrular",Answer2="Çifte Kuğular",Answer3="Aşk Böcekleri",Answer4="Aşk Güvercinleri", CorrectAnswer="Çifte Kumrular", Questions="Birbirlerini çok seven çok iyi anlaşan çiftler için ne denir?",},
            new Question{QuestionId=73 , AnswerId=73, Answer1="Nişanlı",Answer2="Uzatmalı sevgili",Answer3="Yarı Evli",Answer4="Yarı Sevgili", CorrectAnswer="Uzatmalı sevgili", Questions="Evlenmeye karar vermeyip uzun süre sevgili olarak kalanlara ne denir?",},
            new Question{QuestionId=74 , AnswerId=74, Answer1="Beyblade",Answer2="Jetgiller",Answer3="Taş Devri",Answer4="Susam Sokağı", CorrectAnswer="Jetgiller", Questions="Uzay Çağını yaşayan bir aileyi anlatan çizgi filmin adı nedir?",},
            new Question{QuestionId=75 , AnswerId=75, Answer1="Cahit Arf",Answer2="Gödel",Answer3="John Nash",Answer4="Hilbert", CorrectAnswer="John Nash", Questions="Akıl oyunları filmine hayat hikâyesiyle Oscar kazandıran Oyun Teorisi ile Nobel ödülü alan matematikçi kimdir?",},
            new Question{QuestionId=76 , AnswerId=76, Answer1="119",Answer2="81",Answer3="100",Answer4="150", CorrectAnswer="119", Questions="Rüzgâr saatte kaç kilometre hızla esmeye başladığında kasırga olarak adlandırılır?",},
            new Question{QuestionId=77 , AnswerId=77, Answer1="Ağrı",Answer2="Çanakkale",Answer3="Zonguldak ",Answer4="Kastamonu", CorrectAnswer="Kastamonu", Questions="Çanakkale içinde aynalı çarşı türküsü hangi yöreye aittir?",},
            new Question{QuestionId=78 , AnswerId=78, Answer1="Timsah",Answer2="Fil",Answer3="Keçi",Answer4="Tavşan", CorrectAnswer="Tavşan", Questions="İnsanlarda, normalden daha uzun ve önde duran ön dişler hangi hayvana benzetilir?",},
            new Question{QuestionId=79 , AnswerId=79, Answer1="Birey Oyunu",Answer2="Takım Oyunu",Answer3="Top Oyunu",Answer4="Hareket Oyunu", CorrectAnswer="Takım Oyunu", Questions="Futbol, basketbol, voleybol gibi toplu olarak oynanan oyunlara ne ad verilir?",},
            new Question{QuestionId=80 , AnswerId=80, Answer1="Voleybol",Answer2="Hokey",Answer3="Karaoke",Answer4="Bowling", CorrectAnswer="Karaoke", Questions="Eğlenmek amacıyla şarkıların melodileri üzerine ekranda yazan takip ederek şarkı söylemeye ne ad verilir?",},
            new Question{QuestionId=81 , AnswerId=81, Answer1="1",Answer2="20",Answer3="70",Answer4="200", CorrectAnswer="20", Questions="Bir kişinin cumhurbaşkanlığına aday gösterilebilmesi kaç milletvekilinin yazılı teklifi ile mümkündür?",},
            new Question{QuestionId=82 , AnswerId=82, Answer1="Klostrofobi",Answer2="Namofobi",Answer3="Panfobi",Answer4="Teknofobi", CorrectAnswer="Namofobi", Questions="Cep telefonuyla iletişim olanağından uzak kalma korkusuna ne ad verilir?",},
            new Question{QuestionId=83 , AnswerId=83, Answer1="Yüz görümlülüğü",Answer2="Başlık Parası",Answer3="Duvak Parası",Answer4="Çeyiz", CorrectAnswer="Yüz görümlülüğü", Questions="Damadın, duvağı açmadan önce taktığı takıya ne ad verilir?",},
            new Question{QuestionId=84 , AnswerId=84, Answer1="Süper Lig",Answer2="El Clasico",Answer3="Serie A",Answer4="Bundesliga", CorrectAnswer="El Clasico", Questions="İspanya’da oynanan Barcelona-Real Madrid maçlarına ne ad verilir?",},
            new Question{QuestionId=85 , AnswerId=85, Answer1="Okuma",Answer2="Seçme ve Seçilme",Answer3="Boşanma",Answer4="Araba kullanma", CorrectAnswer="Boşanma", Questions="Türkiye’de kadınlar hangi hakkı diğerlerinden önce kazanmıştır?",},
            new Question{QuestionId=86 , AnswerId=86, Answer1="Sivas",Answer2="Roma",Answer3="Tokat",Answer4="York", CorrectAnswer="Tokat", Questions="Roma İmparatorluğu Sezar’ın Geldim, gördüm, yendim sözlerini kullandığı Zela Savaşı hangi şehirde gerçekleştirmiştir?",},
            new Question{QuestionId=87 , AnswerId=87, Answer1="Silah",Answer2="Yedek Asker",Answer3="Araç",Answer4="Komutan", CorrectAnswer="Yedek Asker", Questions="Kışlanın önünde redif sesi var dizesinde geçen redif ne demektir?",},
            new Question{QuestionId=88 , AnswerId=88, Answer1="Dişçi",Answer2="Ajan",Answer3="Doktor",Answer4="Mühendis", CorrectAnswer="Dişçi", Questions="Elektrikli sandalyeyi keşfeden Alfred Southwick’in mesleği neydi?",},
            new Question{QuestionId=89 , AnswerId=89, Answer1="Karabaş",Answer2="Pamuk",Answer3="Nez",Answer4="Sarıkız", CorrectAnswer="Sarıkız", Questions="Halk arasında hem inek anlamına gelen gemde ineklere en sık verilen isim hangisidir?",},
            new Question{QuestionId=90 , AnswerId=90, Answer1="Nadir Beden",Answer2="Küçük Beden",Answer3="İnce Beden",Answer4="Sıfır Beden", CorrectAnswer="Sıfır Beden", Questions="Kıyafetlerde 32-34 arası beden ölçüsüne ne ad verilir?",},
            new Question{QuestionId=91 , AnswerId=91, Answer1="Emniyet Teşkilatı",Answer2="Cumhurbaşkanlığı",Answer3="Türk Silahlı Kuvvetleri",Answer4="İçişleri Bakanlığı", CorrectAnswer="Cumhurbaşkanlığı", Questions="864 Rakımlı Tepe terimi hangi kurumu tanımlamak için kullanılır?",},
            new Question{QuestionId=92 , AnswerId=92, Answer1="Kırmızı",Answer2="Dudak",Answer3="Güzel",Answer4="Krem", CorrectAnswer="Kırmızı", Questions="Kadınların makyaj malzemesi olarak kullandığı, dilimize Fransızcadan geçen rujun kelime anlamı nedir?",},
            new Question{QuestionId=93 , AnswerId=93, Answer1="Anemi",Answer2="Çiçek",Answer3="Parkinson",Answer4="Reflü", CorrectAnswer="Parkinson", Questions="Hangi hastalık adını onu bulan kişinin soyadından almıştır?",},
            new Question{QuestionId=94 , AnswerId=94, Answer1="Cumartesi Pazarı",Answer2="Akşam Pazarı",Answer3="Pazar Sonrası",Answer4="Toptan Satış", CorrectAnswer="Akşam Pazarı", Questions="Pazar yerleri toplanırken tezgâhta kalmış ürünlerin ucuz fiyatla satılmasına ne ad verilir?",},
            new Question{QuestionId=95 , AnswerId=95, Answer1="At",Answer2="Tavşan",Answer3="Balık",Answer4="Kuş", CorrectAnswer="Kuş", Questions="Milli Piyango’nun ambleminde yer alan simge hangisidir?",},
            new Question{QuestionId=96 , AnswerId=96, Answer1="Öksürmek",Answer2="Hapşırmak",Answer3="Esnemek",Answer4="Parmak Emmek", CorrectAnswer="Öksürmek", Questions="Anne karnındaki bebek hangisini yapamaz?",},
            new Question{QuestionId=97 , AnswerId=97, Answer1="Rusya",Answer2="Norveç",Answer3="Küba",Answer4="Finlandiya", CorrectAnswer="Norveç", Questions="Atatürk gibi düşünmek hangi ülkede kullanılan bir deyimdir?",},
            new Question{QuestionId=98 , AnswerId=98, Answer1="Pusula",Answer2="Kılavuz",Answer3="Atlas",Answer4="Kitap", CorrectAnswer="Kılavuz", Questions="Hayatta en hakiki mursit ilimdir sözündeki mürsit ne anlama gelir ?",},
            new Question{QuestionId=99 , AnswerId=99, Answer1="Michelangelo",Answer2="Pablo Picasso",Answer3="Leonardo Da Vinci",Answer4="Vincent Van Gogh", CorrectAnswer="Leonardo Da Vinci", Questions="Bill Gates'in 30.8 milyon dolar ödeyerek aldığı bilimsel yazılarla dolu 72 sayfalık Codex Hammer adlı kitabın yazarı kimdir?",},
            new Question{QuestionId=100 , AnswerId=100, Answer1="Mavi Bilye",Answer2="Yeşil Boncuk",Answer3="Siyah Nokta",Answer4="Mavi Top", CorrectAnswer="Mavi Bilye", Questions="1972’de Apollo 17 uzay aracı mürettebatınca çekilen ve yerküreyi bütün olarak gösteren ünlü fotoğrafın adı nedir?",},
            new Question{QuestionId=101 , AnswerId=101, Answer1="Demir Parmaklık",Answer2="Gizli Oda",Answer3="Terazi",Answer4="Kılıç", CorrectAnswer="Demir Parmaklık", Questions="Dilimize Fransızcadan geçen avukatların kayıtlı oldukları meslek kuruluşu baro’nun kelime anlamı nedir?",},
            new Question{QuestionId=102 , AnswerId=102, Answer1="Bollywood Levhası",Answer2="Amerika Levhası",Answer3="Lee Levhası",Answer4="Hollywood Levhası", CorrectAnswer="Hollywood Levhası", Questions="Lee Dağı üzernde yer alan dünyaca ünlü kent simgesi hangisidir?",},
            new Question{QuestionId=103 , AnswerId=103, Answer1="+23",Answer2="+533",Answer3="+90",Answer4="+453", CorrectAnswer="+90", Questions="Türkiye'nin uluslararası telefon kodu kaçtır?",},
            new Question{QuestionId=104 , AnswerId=104, Answer1="Kerim Abdül Cabbar",Answer2="Michael Jordan",Answer3="Kobe Bryant",Answer4="Lebron James", CorrectAnswer="Kerim Abdül Cabbar", Questions="NBA tarihinin en skorer oyuncusu kimdir?",},
            new Question{QuestionId=105 , AnswerId=105, Answer1="Hidrojen",Answer2="Oksijen",Answer3="Nitrojen",Answer4="Helyum", CorrectAnswer="Nitrojen", Questions="Otomobillerde hava yastıklarının şişmesi için kullanılan gaz hangisidir?",},
            new Question{QuestionId=106 , AnswerId=106, Answer1="Ali Şen",Answer2="Şener Şen",Answer3="Kemal Sunal",Answer4="Halit Akçatepe", CorrectAnswer="Şener Şen", Questions="Süt Kardeşler filminde ''Seni hiç sevmedim süt oğlan, babanı da sevmezdim'' repliğini söyleyen oyuncu kimdir?",},
            new Question{QuestionId=107 , AnswerId=107, Answer1="Ülkeleri",Answer2="Kıtaları",Answer3="Spor dallarını",Answer4="Madalyaları", CorrectAnswer="Kıtaları", Questions="Olimpiyat bayrağı üzerinde bulunan iç içe geçmiş 5 renkli halka hangilerini temsil eder?",},
            new Question{QuestionId=108 , AnswerId=108, Answer1="Karnıyarık ve Cacık",Answer2="Patates ve Köfte",Answer3="Makarna ve Dolma",Answer4="Kuru Fasulye ve Pilav", CorrectAnswer="Kuru Fasulye ve Pilav", Questions="Atatürk'ün okulda alıştığını söylediği ve çok sevdiği yemek hangisidir?",},
            new Question{QuestionId=109 , AnswerId=109, Answer1="Emlak",Answer2="Sanayi",Answer3="Sinema",Answer4="Turizm", CorrectAnswer="Emlak", Questions="Hollywood tabelası 1923 yılında hangi sektörün tanıtımı için dikilmiştir?",},
            new Question{QuestionId=110 , AnswerId=110, Answer1="Hititler",Answer2="Elamlar",Answer3="Sümerler",Answer4="Urartular", CorrectAnswer="Sümerler", Questions="Aşağıda Verilen İlk Çağ Uygarlıklarından Hangisi Yazıyı İcat Etmiştir?",},
            new Question{QuestionId=111 , AnswerId=111, Answer1="NATO",Answer2="WHO",Answer3="UNICEF",Answer4="UHW", CorrectAnswer="WHO", Questions="Aşağıdakilerden Hangisi Dünya Sağlık Örgütünün Kısaltılmış İsmidir?",},
            new Question{QuestionId=112 , AnswerId=112, Answer1="Köknar",Answer2="Söğüt",Answer3="Meşe",Answer4="Gürgen", CorrectAnswer="Söğüt", Questions="Aspirinin Hammaddesi Nedir?",},
            new Question{QuestionId=113 , AnswerId=113, Answer1="Kudüs",Answer2="Roma",Answer3="Mekke",Answer4="İstanbul", CorrectAnswer="Kudüs", Questions="Üç Büyük Dince Kutsal Sayılan Şehir Hangisidir?",},

        };
    }

    private static List<Question> unansweredQuestions;
    private Question currentQuestion;
    void Start()
    {
        Time.timeScale = 1f;
        if (unansweredQuestions ==null || unansweredQuestions.Count==0)
        {
            unansweredQuestions = _questions.ToList<Question>();
        }
        SetCurrentQuestion();        
    }
   
    public void SetCurrentQuestion()
    {
        if (true)
        {
            targetTime = 30;
            int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);

            currentQuestion = unansweredQuestions[randomQuestionIndex];
            
            question.text = currentQuestion.Questions.ToString();
            if (question.text == currentQuestion.Questions.ToString())
            {
                answerButton1.GetComponentInChildren<Text>().text = currentQuestion.Answer1.ToString();
                answerButton2.GetComponentInChildren<Text>().text = currentQuestion.Answer2.ToString();
                answerButton3.GetComponentInChildren<Text>().text = currentQuestion.Answer3.ToString();
                answerButton4.GetComponentInChildren<Text>().text = currentQuestion.Answer4.ToString();
                answerText.text = currentQuestion.AnswerId.ToString();
                trueText.text = currentQuestion.CorrectAnswer;
            }
        }        
        count = PlayerPrefs.GetInt("Soru Sayısı") + 1;
        PlayerPrefs.SetInt("Soru Sayısı", count);
        RamdomQuestion();
        Debug.Log("Liste Uzunluğu : " + unansweredQuestions.Count + " " + count + " .İNCİ SORU");
        //playAgain = false;
    }

    public void renk_degis_tespiti(string cevap_dogru)
    {
        if (cevap_dogru == currentQuestion.Answer1.ToString())
        {
            answerButton1.GetComponent<Image>().color = Color.green;
        }
        else if (cevap_dogru == currentQuestion.Answer2.ToString())
        {
            answerButton2.GetComponent<Image>().color = Color.green;
        }
        else if (cevap_dogru == currentQuestion.Answer3.ToString())
        {
            answerButton3.GetComponent<Image>().color = Color.green;
        }
        else if (cevap_dogru == currentQuestion.Answer4.ToString())
        {
            answerButton4.GetComponent<Image>().color = Color.green;
        }
    }
    public void dogru_renk_degis(string cevap_dogru)
    {
        renk_degis_tespiti(cevap_dogru);

    }

    public void yanlis_renk_degis(string cevap_yanlis)
    { 
        renk_degis_tespiti(currentQuestion.CorrectAnswer);

        if (cevap_yanlis == currentQuestion.Answer1.ToString())
        {
            answerButton1.GetComponent<Image>().color = Color.red;
        }
        else if (cevap_yanlis == currentQuestion.Answer2.ToString())
        {
            answerButton2.GetComponent<Image>().color = Color.red;
        }
        else if (cevap_yanlis == currentQuestion.Answer3.ToString())
        {
            answerButton3.GetComponent<Image>().color = Color.red;
        }
        else if (cevap_yanlis == currentQuestion.Answer4.ToString())
        {
            answerButton4.GetComponent<Image>().color = Color.red;
        }

    }
    public void remove()
    {
        unansweredQuestions.Remove(currentQuestion);
    }
    public void RamdomQuestion()
    {
        if (PlayerPrefs.GetInt("Soru Sayısı",count)==16)
        {
            count = 0;
            PlayerPrefs.SetInt("Soru Sayısı", count);
            panelManager.youWin();
            Debug.Log("Oyun bitti");
            oyunBitti = true;
            
        }
    }

    //30 saniye sonra bir daha ki soruya geç.
    
    float delay = 0.3f;
    public void playAgain_btn_options()
    {
        PlayerPrefs.SetInt("geçici_skor", 0);
        PlayerPrefs.SetInt("Doğru Sayısı", 0); 
        PlayerPrefs.SetInt("Yanlış Sayısı", 0);
        PlayerPrefs.SetFloat("Güncel can", 3f);
        StartCoroutine(playAgainOptions());
    }
    public void playAgain_btn_gameOver()
    {
        PlayerPrefs.SetInt("geçici_skor", 0);
        PlayerPrefs.SetInt("Doğru Sayısı", 0); 
        PlayerPrefs.SetInt("Yanlış Sayısı", 0);
        PlayerPrefs.SetFloat("Güncel can", 3f);
        StartCoroutine(playAgainGameOver());
    }
    public void playAgain_btn_youWin()
    {
        PlayerPrefs.SetInt("geçici_skor", 0);
        PlayerPrefs.SetInt("Doğru Sayısı", 0); 
        PlayerPrefs.SetInt("Yanlış Sayısı", 0);
        PlayerPrefs.SetFloat("Güncel can", 3f);
        StartCoroutine(playAgainYouWin());
    }
    public void PlayGame()
    {
        PlayerPrefs.SetInt("geçici_skor", 0);
        PlayerPrefs.SetInt("Doğru Sayısı", 0);
        PlayerPrefs.SetInt("Yanlış Sayısı", 0);
        PlayerPrefs.SetFloat("Güncel can", 3f);
        StartCoroutine(playGame());
    }

    IEnumerator playGame()
    {

        count = 0;
        PlayerPrefs.SetInt("Soru Sayısı",count);
        playAgain = true;
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    IEnumerator playAgainOptions()
    {
        count = 0;
        PlayerPrefs.SetInt("Soru Sayısı", count);
        panelManager.backOptions_btn();
        Time.timeScale = 1;
        playAgain = true;

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator playAgainGameOver()
    {
        count = 0;
        PlayerPrefs.SetInt("Soru Sayısı", count);
        panelManager.gameOver();
        panelManager.youWinPannel.SetActive(true);
        playAgain = true;
        Time.timeScale = 1;

        yield return new WaitForSeconds(delay);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator playAgainYouWin()
    {
        count = 0;
        PlayerPrefs.SetInt("Soru Sayısı", count);
        panelManager.youWin();
        Time.timeScale = 1;
        playAgain = true;
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
