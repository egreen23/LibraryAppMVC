CREATE PROCEDURE [dbo].[spBook_FilterByPrice]
	@Price decimal(18,2)
AS
begin
	select [Id], [Titolo], [Quantita], [DataPubblicazione], [Genere], [Prezzo], [AuthorId]
	from Book b
	where b.Prezzo = @Price
end
