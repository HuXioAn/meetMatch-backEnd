# meetMatch backend api design

> **request**: elements passed from frontend to backend
> 
> **reply**: elements given by the backend 
>
> Elements marked with \* are optional, can be left blank.

## create time table 

`POST /api/timeTable/`

request:

- meeting name
- date selection
- time range
- \*max collaborator
- \*email address

``` json

{
    "meetingName": "next group meeting",
    "dateSelection": ["2024-01-25", "2024-01-26", "2024-01-28"],
    "timeRange": [8, 17],
    "maxCollaborator": 10,
    "email": "example@kth.se"
}
```

reply:

- state
<!-- - table uuid -->
- table visit token
- table manage token

```json
{
    "state": 0,
    "tableVisitToken": "",
    "tableManageToken": ""
}

```

## visit a time table

`GET /api/timeTable/{visitToken}`

request:

- table visit token (in url)

<!-- ```json
{
    "tableVisitToken": ""
}

``` -->

reply:

- state
- meeting name
- date selection
- time range
- \*existing selection

```json
{
    "state": 0,
    "meetingName": "next group meeting",
    "dateSelection": ["2024-01-25", "2024-01-26", "2024-01-28"],
    "timeRange": [8, 17],
    "existSelection": [
        { 
            "color": "green",
            "slots": [
                {
                    "startTime": "2024-01-25T18:51:41.406Z",
                    "endTime": "2024-01-25T19:51:41.406Z"
                },
                {
                    "startTime": "2024-01-25T20:51:41.406Z",
                    "endTime": "2024-01-25T21:51:41.406Z"
                },
                {
                    "startTime": "",
                    "endTime": ""
                }
            ]
        },
        {
            "color": "",
            "slots": [
            ]
        }
        
    ]
}
```


## update a time table

`POST /api/timeTable/update/{visitToken}`

request:

- table visit token (in url)
- selection

```json
{
    "selection": [
        {
            "startTime": "2024-01-25T18:51:41.406Z",
            "endTime": "2024-01-25T19:51:41.406Z"
        },
        {
            "startTime": "2024-01-26T18:51:41.406Z",
            "endTime": "2024-01-26T19:51:41.406Z"
        },
        {
            "startTime": "",
            "endTime": ""
        }
    ]

}
```

reply:

- state

```json
{
    "state": 0
}
```

## manage a time table

`PUT /api/timeTable/manage/{manageToken}`

request:

- table manage token (in url)
- meeting name
- date selection
- time range
- \*max collaborator
- \*email address

```json
{
    "meetingName": "next group meeting",
    "dateSelection": ["2024-01-25", "2024-01-26", "2024-01-28"],
    "timeRange": [8, 17],
    "maxCollaborator": 10,
    "email": "example@kth.se"
}
```

reply:

- state
- table visit token
- table manage token

```json
{
    "state": 0,
    "tableVisitToken": "",
    "tableManageToken": ""
}
```

## get the results

`GET /api/timeTable/result/{visitToken}`

request:

- table visit token (in url)

<!-- ```json
{
    "tableVisitToken": ""
}
``` -->

reply:

- state
- existing selection
- recommended results

```json
{
    "state": 0,
    "existSelection": [
        { 
            "color": "green",
            "slots": [
                {
                    "startTime": "2024-01-25T18:51:41.406Z",
                    "endTime": "2024-01-25T19:51:41.406Z"
                },
                {
                    "startTime": "2024-01-26T18:51:41.406Z",
                    "endTime": "2024-01-26T19:51:41.406Z"
                },
                {
                    "startTime": "",
                    "endTime": ""
                }
            ]
        },
        {
            "color": "",
            "slots": [
            ]
        }   
    ],
    "recommendedResults": [
        {
            "startTime": "2024-01-25T18:51:41.406Z",
            "endTime": "2024-01-25T19:51:41.406Z"
        },
        {
            "startTime": "2024-01-26T18:51:41.406Z",
            "endTime": "2024-01-26T19:51:41.406Z"
        },
        {
            "startTime": "",
            "endTime": ""
        }
    ]
}
```

## delete a time table

`DELETE /api/timeTable/{manageToken}`

request:

- table manage token (in url)

<!-- ```json
{
    "tableManageToken": ""
}
``` -->

reply:

- state

```json
{
    "state": 0
}
```


