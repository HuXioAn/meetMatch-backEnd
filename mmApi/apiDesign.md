# meetMatch backend api design

> **request**: elements passed from frontend to backend
> 
> **reply**: elements given by the backend 
>
> Elements marked with \* are optional.

## create time table 

request:

- meeting name
- date selection
- time range
- \*max collaborator
- \*email address

``` json

{
    "meetingName": "next group meeting",
    "dateSelection": ["2024.2.1", "2024.2.2", "2024.2.5"],
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

request:

- table visit token

```json
{
    "tableVisitToken": ""
}

```

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
    "dateSelection": ["2024.2.1", "2024.2.2", "2024.2.5"],
    "timeRange": [8, 17],
    "existSelection": [
        { 
            "color": "green",
            "slots": [
                {
                    "startTime": "2024.2.1-10:15",
                    "endTime": "2024.2.1-12:15"
                },
                {
                    "startTime": "2024.2.1-14:15",
                    "endTime": "2024.2.1-16:15"
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

request:

- table visit token
- selection

```json
{
    "tableVisitToken": "",
    "selection": [
        {
            "startTime": "2024.2.1-10:15",
            "endTime": "2024.2.1-12:15"
        },
        {
            "startTime": "2024.2.1-14:15",
            "endTime": "2024.2.1-16:15"
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

request:

- table manage token
- meeting name
- date selection
- time range
- \*max collaborator
- \*email address

```json
{
    "tableManageToken": "",
    "meetingName": "next group meeting",
    "dateSelection": ["2024.2.1", "2024.2.2", "2024.2.5"],
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

request:

- table visit token

```json
{
    "tableVisitToken": ""
}
```

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
                    "startTime": "2024.2.1-10:15",
                    "endTime": "2024.2.1-12:15"
                },
                {
                    "startTime": "2024.2.1-14:15",
                    "endTime": "2024.2.1-16:15"
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
            "startTime": "2024.2.1-10:15",
            "endTime": "2024.2.1-12:15"
        },
        {
            "startTime": "2024.2.1-14:15",
            "endTime": "2024.2.1-16:15"
        },
        {
            "startTime": "",
            "endTime": ""
        }
    ]
}
```

## delete a time table

request:

- table manage token

```json
{
    "tableManageToken": ""
}
```

reply:

- state

```json
{
    "state": 0
}
```


