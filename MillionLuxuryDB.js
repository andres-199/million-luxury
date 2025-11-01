/*
 Navicat Premium Dump Script

 Source Server         : local
 Source Server Type    : MongoDB
 Source Server Version : 80201 (8.2.1)
 Source Host           : localhost:27017
 Source Schema         : MillionLuxuryDB

 Target Server Type    : MongoDB
 Target Server Version : 80201 (8.2.1)
 File Encoding         : 65001

 Date: 31/10/2025 23:22:39
*/


// ----------------------------
// Collection structure for Owners
// ----------------------------
db.getCollection("Owners").drop();
db.createCollection("Owners");

// ----------------------------
// Documents of Owners
// ----------------------------
db.getCollection("Owners").insert([ {
    _id: ObjectId("6904efe3eb49ef14d71909a2"),
    Name: "Jack",
    Address: "123 Main Street, New York, NY 10001",
    Photo: "https://example.com/photos/123.jpg",
    Birthday: ISODate("1980-05-15T00:00:00.000Z"),
    Properties: [ ],
    CreatedAt: ISODate("2025-10-31T17:20:35.425Z"),
    UpdatedAt: null
} ]);
db.getCollection("Owners").insert([ {
    _id: ObjectId("69052a164e09bec9aba3cbab"),
    Name: "Jack-2",
    Address: "123 Main Street, New York, NY 10001 -2",
    Photo: "https://example.com/photos/123-2.jpg",
    Birthday: ISODate("1980-05-15T00:00:00.000Z"),
    Properties: [ ],
    CreatedAt: ISODate("2025-10-31T21:28:54.419Z"),
    UpdatedAt: null
} ]);

// ----------------------------
// Collection structure for Properties
// ----------------------------
db.getCollection("Properties").drop();
db.createCollection("Properties");
db.getCollection("Properties").createIndex({
    "$**": "text"
}, {
    name: "Name_text_Address_text",
    weights: {
        Address: Int32("1"),
        Name: Int32("1")
    },
    default_language: "english",
    language_override: "language",
    textIndexVersion: Int32("3")
});
db.getCollection("Properties").createIndex({
    Price: Int32("1")
}, {
    name: "Price_1"
});

// ----------------------------
// Documents of Properties
// ----------------------------
db.getCollection("Properties").insert([ {
    _id: ObjectId("6904f09ceb49ef14d71909a3"),
    Name: "Luxury Downtown Apartment",
    Address: "456 Park Avenue, New York, NY 10022",
    Price: Decimal128("1500000"),
    CodeInternal: "PROP-000",
    Year: Int32("2020"),
    OwnerId: ObjectId("6904efe3eb49ef14d71909a2"),
    Images: [ ],
    Traces: [ ],
    CreatedAt: ISODate("2025-10-31T17:23:40.173Z"),
    UpdatedAt: null
} ]);
db.getCollection("Properties").insert([ {
    _id: ObjectId("6905188ac624df48a173d1eb"),
    Name: "Luxury Beach Villa",
    Address: "123 Ocean Drive, Miami Beach",
    Price: Decimal128("2500000"),
    CodeInternal: "PROP-001",
    Year: Int32("2020"),
    OwnerId: ObjectId("6904efe3eb49ef14d71909a2"),
    Images: [ ],
    Traces: [ ],
    CreatedAt: ISODate("2025-10-31T20:14:02.57Z"),
    UpdatedAt: null
} ]);
db.getCollection("Properties").insert([ {
    _id: ObjectId("690518cec624df48a173d1ec"),
    Name: "Modern Downtown Apartment",
    Address: "456 Main Street, New York",
    Price: Decimal128("850000"),
    CodeInternal: "PROP-002",
    Year: Int32("2018"),
    OwnerId: ObjectId("6904efe3eb49ef14d71909a2"),
    Images: [ ],
    Traces: [ ],
    CreatedAt: ISODate("2025-10-31T20:15:10.081Z"),
    UpdatedAt: null
} ]);
db.getCollection("Properties").insert([ {
    _id: ObjectId("690518f2c624df48a173d1ed"),
    Name: "Luxury Mountain Chalet",
    Address: "789 Alpine Road, Aspen",
    Price: Decimal128("4200000"),
    CodeInternal: "PROP-003",
    Year: Int32("2022"),
    OwnerId: ObjectId("6904efe3eb49ef14d71909a2"),
    Images: [ ],
    Traces: [ ],
    CreatedAt: ISODate("2025-10-31T20:15:46.391Z"),
    UpdatedAt: null
} ]);
db.getCollection("Properties").insert([ {
    _id: ObjectId("69051901c624df48a173d1ee"),
    Name: "Cozy Beach Cottage",
    Address: "321 Sunset Boulevard, Malibu",
    Price: Decimal128("1200000"),
    CodeInternal: "PROP-004",
    Year: Int32("2015"),
    OwnerId: ObjectId("6904efe3eb49ef14d71909a2"),
    Images: [ ],
    Traces: [ ],
    CreatedAt: ISODate("2025-10-31T20:16:01.431Z"),
    UpdatedAt: null
} ]);
db.getCollection("Properties").insert([ {
    _id: ObjectId("6905190ec624df48a173d1ef"),
    Name: "Urban Penthouse",
    Address: "555 Main Street, Chicago",
    Price: Decimal128("3800000"),
    CodeInternal: "PROP-005",
    Year: Int32("2021"),
    OwnerId: ObjectId("6904efe3eb49ef14d71909a2"),
    Images: [ ],
    Traces: [ ],
    CreatedAt: ISODate("2025-10-31T20:16:14.946Z"),
    UpdatedAt: null
} ]);
db.getCollection("Properties").insert([ {
    _id: ObjectId("6905208d051de906f05349d6"),
    Name: "Urban Penthouse rouse",
    Address: "506 Street, Chicago north",
    Price: Decimal128("6800000"),
    CodeInternal: "PROP-006",
    Year: Int32("2020"),
    OwnerId: ObjectId("6904efe3eb49ef14d71909a2"),
    Images: [ ],
    Traces: [ ],
    CreatedAt: ISODate("2025-10-31T20:48:13.231Z"),
    UpdatedAt: null
} ]);
db.getCollection("Properties").insert([ {
    _id: ObjectId("69052a4c4e09bec9aba3cbad"),
    Name: "Urban Penthouse rouse-2",
    Address: "506 Street, Chicago north-2",
    Price: Decimal128("6800000"),
    CodeInternal: "PROP-007",
    Year: Int32("2020"),
    OwnerId: ObjectId("6904efe3eb49ef14d71909a2"),
    Images: [ ],
    Traces: [ ],
    CreatedAt: ISODate("2025-10-31T21:29:48.805Z"),
    UpdatedAt: null
} ]);

// ----------------------------
// Collection structure for PropertyImages
// ----------------------------
db.getCollection("PropertyImages").drop();
db.createCollection("PropertyImages");

// ----------------------------
// Documents of PropertyImages
// ----------------------------
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("6904f1ecc270b15e64414e6e"),
    PropertyId: ObjectId("6904f09ceb49ef14d71909a3"),
    File: "https://foreignbuyerswatch.com/wp-content/uploads/2019/07/Capture-d%E2%80%99e%CC%81cran-2019-07-26-a%CC%80-13.14.52.png",
    Enabled: true,
    CreatedAt: ISODate("2025-10-31T17:29:16.14Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("6904f211c270b15e64414e6f"),
    PropertyId: ObjectId("6905188ac624df48a173d1eb"),
    File: "https://st3.idealista.com/news/archivos/styles/fullwidth_xl/public/2018-02/news/Facebook_Noti_1200x627_inmueble_lujo.jpg?VersionId=9NfRTQ65s3_WzLxrfhhO0KTiqEyfoj4R&itok=eIlDuGIY",
    Enabled: true,
    CreatedAt: ISODate("2025-10-31T17:29:53.753Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("6904f216c270b15e64414e70"),
    PropertyId: ObjectId("6904f09ceb49ef14d71909a3"),
    File: "https://st3.idealista.com/news/archivos/styles/fullwidth_xl/public/2025-01/images/1246650703.jpg?VersionId=Orns9nI4z0bHC53owFW5Bs5chL67R9.Z&itok=jVsXW7ua",
    Enabled: true,
    CreatedAt: ISODate("2025-10-31T17:29:58.377Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("69052a204e09bec9aba3cbac"),
    PropertyId: ObjectId("6905188ac624df48a173d1eb"),
    File: "https://hurghadiansproperty.com/wp-content/uploads/2024/08/hurghadiansproperty_A_stunning_contemporary_villa_in_Marbella_S_a1848e18-b163-47ee-881c-d7b473fe7e49.webp",
    Enabled: true,
    CreatedAt: ISODate("2025-10-31T21:29:04.522Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("6905528c457ae7895d552737"),
    PropertyId: ObjectId("690518cec624df48a173d1ec"),
    File: "https://assets-us-01.kc-usercontent.com/28e7bd12-5b30-009d-524e-785407f8bd6e/85561181-ad42-4bf2-90d5-98dc63a73487/17010%20Clearlake%20Ave%20Bradenton-print-001-018-Front%20dusk-4200x3150-300dpi.jpg?w=1600&h=900&fit=crop",
    Enabled: true,
    CreatedAt: ISODate("2025-11-01T00:21:32.589Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("690552a8457ae7895d552738"),
    PropertyId: ObjectId("690518cec624df48a173d1ec"),
    File: "https://assets-us-01.kc-usercontent.com/28e7bd12-5b30-009d-524e-785407f8bd6e/886a1b11-d413-4878-acce-e4d6563a9e92/Margo%20IV%20Elevation%20H%20MO%20CS7%20S3D_2025.jpg?w=800&h=540&fit=crop",
    Enabled: true,
    CreatedAt: ISODate("2025-11-01T00:22:00.112Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("69055470457ae7895d552739"),
    PropertyId: ObjectId("690518f2c624df48a173d1ed"),
    File: "https://wilmasb.com/wp-content/uploads/2022/03/21-02_chillout_vista2_post.jpg",
    Enabled: true,
    CreatedAt: ISODate("2025-11-01T00:29:36.74Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("690554a1457ae7895d55273a"),
    PropertyId: ObjectId("69051901c624df48a173d1ee"),
    File: "https://res.cloudinary.com/rock-agency/image/fetch/w_1100,c_fill,g_auto,f_auto/https://www.infolio.com.au/wp-content/uploads/2024/08/infolio-website-blog-image-8.jpg",
    Enabled: true,
    CreatedAt: ISODate("2025-11-01T00:30:25.021Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("690554cb457ae7895d55273b"),
    PropertyId: ObjectId("6905190ec624df48a173d1ef"),
    File: "https://media-production.lp-cdn.com/cdn-cgi/image/format=auto,quality=85,fit=scale-down,width=1920/https://media-production.lp-cdn.com/media/yomr4kmolf7sdkk2psvy",
    Enabled: true,
    CreatedAt: ISODate("2025-11-01T00:31:07.283Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("69055a0d457ae7895d55273c"),
    PropertyId: ObjectId("6905190ec624df48a173d1ef"),
    File: "https://foreignbuyerswatch.com/wp-content/uploads/2019/07/Capture-d%E2%80%99e%CC%81cran-2019-07-26-a%CC%80-13.14.52.png",
    Enabled: true,
    CreatedAt: ISODate("2025-11-01T00:53:33.902Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("69055a21457ae7895d55273d"),
    PropertyId: ObjectId("6905208d051de906f05349d6"),
    File: "https://st3.idealista.com/news/archivos/styles/fullwidth_xl/public/2018-02/news/Facebook_Noti_1200x627_inmueble_lujo.jpg?VersionId=9NfRTQ65s3_WzLxrfhhO0KTiqEyfoj4R&itok=eIlDuGIY",
    Enabled: true,
    CreatedAt: ISODate("2025-11-01T00:53:53.836Z"),
    UpdatedAt: null
} ]);
db.getCollection("PropertyImages").insert([ {
    _id: ObjectId("69055a52457ae7895d55273e"),
    PropertyId: ObjectId("69052a4c4e09bec9aba3cbad"),
    File: "https://st3.idealista.com/news/archivos/styles/fullwidth_xl/public/2025-01/images/1246650703.jpg?VersionId=Orns9nI4z0bHC53owFW5Bs5chL67R9.Z&itok=jVsXW7ua",
    Enabled: true,
    CreatedAt: ISODate("2025-11-01T00:54:42.274Z"),
    UpdatedAt: null
} ]);

// ----------------------------
// Collection structure for PropertyTraces
// ----------------------------
db.getCollection("PropertyTraces").drop();
db.createCollection("PropertyTraces");

// ----------------------------
// Documents of PropertyTraces
// ----------------------------
db.getCollection("PropertyTraces").insert([ {
    _id: ObjectId("6904f32c975be63ff8f22acd"),
    DateSale: ISODate("2025-10-31T10:00:00.000Z"),
    Name: "Initial Sale",
    Value: Decimal128("1500000"),
    Tax: Decimal128("75000"),
    PropertyId: ObjectId("6904f09ceb49ef14d71909a3"),
    CreatedAt: ISODate("2025-10-31T17:34:36.916Z"),
    UpdatedAt: null
} ]);
