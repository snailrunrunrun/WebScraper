# WebScraper


### What does the scraper do
This scraper is scaffoled using `RAML` description language.
It supports the following `GET` request:

`Endpoint` /v1/wikipics/{uri}/{tag}

### Documentation
Go to `http://localhost:64483/raml` to get interactive api documentation.

### Demo

Request
```shell
GET /v1/wikipics/Terracotta_Army/a HTTP/1.1
Host: localhost:64483
```

Responses

`202 Accepted`
```json
{
    "Status": "A new scrapping task is scheduled."
}
```

`202 Accepted`
```json
{
    "Status": "Results not available yet. Please try again later."
}
```

`200 OK`
```json
{
    "title": "Terracotta Army",
    "images": [
        "https://upload.wikimedia.org/wikipedia/commons/thumb/4/49/Terracotta_Army%2C_View_of_Pit_1.jpg/220px-Terracotta_Army%2C_View_of_Pit_1.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/4/41/China_edcp_relief_location_map.jpg/250px-China_edcp_relief_location_map.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/2/22/Tomb_of_Emperor_Qin_Shi_Huang.jpg/220px-Tomb_of_Emperor_Qin_Shi_Huang.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Terracotta_Army-China2.jpg/220px-Terracotta_Army-China2.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Terracotta_Army_Pit_1_-_2.jpg/220px-Terracotta_Army_Pit_1_-_2.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/d/df/Recreated_colored_terracotta_warriors.jpg/220px-Recreated_colored_terracotta_warriors.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f3/Bronze_jian_of_the_Terracotta_Army.jpg/260px-Bronze_jian_of_the_Terracotta_Army.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/7/72/Terra_Cotta_Warriors%2C_Guardians_of_China%E2%80%99s_First_Emperor.jpg/150px-Terra_Cotta_Warriors%2C_Guardians_of_China%E2%80%99s_First_Emperor.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Terra_Cotta_Warriors%2C_Guardians_of_China%E2%80%99s_First_Emperor_1.jpg/150px-Terra_Cotta_Warriors%2C_Guardians_of_China%E2%80%99s_First_Emperor_1.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/5/57/Terracotta_Warriors_Exhibition_San_Francisco_2013.jpg/220px-Terracotta_Warriors_Exhibition_San_Francisco_2013.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/Terracotta_army_5256.jpg/400px-Terracotta_army_5256.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/2/22/Officer_Terrakottaarm%C3%A9n.jpg/225px-Officer_Terrakottaarm%C3%A9n.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/6/60/Terracotta_pmorgan.jpg/400px-Terracotta_pmorgan.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Terrakottaarm%C3%A9n.jpg/225px-Terrakottaarm%C3%A9n.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a0/Qin_bronze_chariot_two.jpg/450px-Qin_bronze_chariot_two.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Terracotta_horse_and_two_soldiers.jpg/400px-Terracotta_horse_and_two_soldiers.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/Archer_head.jpg/225px-Archer_head.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Pigments_used_on_terracotta_warriors.jpg/400px-Pigments_used_on_terracotta_warriors.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f3/Terracotta_color_1.jpg/225px-Terracotta_color_1.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/9/91/Terracotta_color_2.jpg/225px-Terracotta_color_2.jpg"
    ],
    "datetimeUTC": "2018-02-12T19:47:25.6130000Z"
}
```

Request
```shell
GET /v1/wikipics/xdfd/a HTTP/1.1
Host: localhost:64483
```

Response 

`400 Bad Request`
```json
{
    "Status": "Bad url."
}
```
