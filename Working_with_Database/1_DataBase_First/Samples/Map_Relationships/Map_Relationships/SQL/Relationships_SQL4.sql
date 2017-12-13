SELECT Book.Name,Book.ISBN,Author.Pk_Author_Id,Author.FullName
FROM Book
left JOIN Author on Book.Pk_Book_Id = Author.Fk_Book_Id
where Book.Name = 'Three Cups of Tea';