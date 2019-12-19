# Diary service

This is an application that will provide new conveniante and efficient way for evaluating and store students progress in educational institutions.

For students it will bring convenient access for observing their progress and some kind of statistic.

Lectures and teachers have to set students points per lesson. It also allow to monitor all their groups and their achievments.

# Architecture overview

![GitHub Logo](/images/app-architecture.png)

https://drive.google.com/file/d/1mc0QvR5uPW-jcwpXSbU4kUExXFeqkiGu/view

## Main components

### 1) Diary api

This service is responsible for modifying and obtaining information about students point and apperal on lesson.

It implements main CRUD operations for `StudentResults` which represent student achievment on lesson.

### 2) Administration api

#### Its the largest part that is responsible for
- Formatting new group, updating existing
- Adding new Lectures/Teachers/Subjects
- Assignig lectures or teachers for specififc group

### 3) Identity service

This service is responsible for enabling access for specific part of application by generating access-token with information about user and its role.

### 4) ReportGenerationJob

Its background task that is triggered by time (In the end of each month).
The main gore of this task is to create report in pdf format for each user and send it to appropriate mail address


## Database

As data storage will be used Azure sql database (that provide convenient way to configure this and other important stuff like authentication and failover)

### ER diagram
###### (https://dbdiagram.io/d/5dfb591aedf08a25543f4085)

![GitHub Logo](/images/er-diagram.png)

## Resilince

### RMA workbook

ID | Component/dependency interaction | Interaction description
------------ | ------------- | -------------
0 | Internet client -> Angular Application | End user connect with angular web application via intenrnet 
1 | User angular client -> Identity service | User send http authorize endpoint from angular application to get OAuth access token
2 | Identity service -> Sql database | Identity service communicate with Azure SQL Database to check user credentials
3 | User angular client -> Diary API service | User send http endpoint with access token from angular application to get/update information about students activity
4 | Diary API service -> Sql database | Diary api service communicate with Azure SQL Database to obtain data about students activities
5 | Admin User angular client -> Identity service | Administrator user trigger authorize endpoint from angular application to get OAuth access token
6 | Admin User angular client -> Administartion api service | Administrator user send request to crud endpoints with access token to get/update user/group information
7 | Administartion api service -> Sql database | Administration api service communicate with Azure sql database to get/update data there
8 | Notification function -> Sql database | Time triggered notification function communicate with Azure sql databse to get students activity information
9 | Notification function -> Send grid (external) | Time triggered notification function communicate with external SendGrid service to send emails to end users 

### CID diagram

https://drive.google.com/file/d/1bzj3JdcI_JtgyNcvk5UOwBpkzKb-nvr1/view?usp=sharing

![GitHub Logo](/images/CID-diagram.png)

### Security model

![GitHub Logo](/images/security-model.png)

