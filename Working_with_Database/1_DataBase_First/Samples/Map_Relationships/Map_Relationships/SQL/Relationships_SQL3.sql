select * from Book;
select * from Author;

SELECT *
FROM Book
LEFT JOIN Author on Book.Pk_Book_Id = Author.Fk_Book_Id;

