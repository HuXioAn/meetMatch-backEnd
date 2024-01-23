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

reply:

- state
<!-- - table uuid -->
- table visit token
- table manage token

## visit a time table

request:

- table visit token

reply:

- state
- meeting name
- date selection
- time range
- \*existing selection


## update a time table

request:

- table visit token
- selection

reply:

- state

## manage a time table

request:

- table manage token
- meeting name
- date selection
- time range
- \*max collaborator
- \*email address

reply:

- state
- table visit token
- table manage token

## get the results

request:

- table visit token

reply:

- state
- existing selection
- recommended results


## delete a time table

request:

- table manage token

reply:

- state


