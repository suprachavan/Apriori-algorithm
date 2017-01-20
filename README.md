# Apriori-algorithm
Analysis of transactions in retail store (Data Mining)

## Project Summary
This code is implementation of Apriori algorithm in C# programming language for the purpose of Association Rule Mining.

The data of customers' transactions over a period of time is read by means of a CSV file nd then the algorithm is applied by assuming a user defined support value. Depending on this value, frequent patterns of the increasing length of the form (Product1, Product2,...Product n) are generated. The confidence valur for a perticular transaction is also calculated by the apriori code.

The frequent patterns indicate the probability that which two products are purchased together by the customer. the more frequent the pattern is more is its support value and we can predict future behavior of the customers and decide the marketing strategies for low-selling products such as offering discounts. This study is also known as "Market Basket Analysis".
