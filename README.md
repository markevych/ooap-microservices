# Diary service

This is an application that will provide new conveniante and efficient way for evaluating and store students progress in educational institutions.

For students it will bring convenient access for observing their progress and some kind of statistic.

Lectures and teachers have to set students points per lesson. It also allow to monitor all their groups and their achievments.

## Architecture overview

![GitHub Logo](/images/architecture.png)

https://drive.google.com/file/d/1mc0QvR5uPW-jcwpXSbU4kUExXFeqkiGu/view

# Main components

# 1) Diary api

This service is responsible for modifying and obtaining information about students point and apperal on lesson.

It implements main CRUD operations for `StudentResults` which represent student achievment on lesson.

# 2) Administration api

### Its the largest part that is responsible for
- Formatting new group, updating existing
- Adding new Lectures/Teachers/Subjects
- Assignig lectures or teachers for specififc group

# 3) Identity service

This service is responsible for enabling access for specific part of application by generating access-token with information about user and its role.

# 4) ReportGenerationJob

Its background task that is triggered by time (In the end of each month).
The main gore of this task is to create report in pdf format for each user and send it to appropriate mail address


## ER diagram

https://dbdiagram.io/d/5db1756802e6e93440f295c2

# Resilince

## CID diagram

![GitHub Logo](/images/CID diagram.png)

https://drive.google.com/file/d/1bzj3JdcI_JtgyNcvk5UOwBpkzKb-nvr1/view?usp=sharing

