using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using DTO;

namespace Controllers;

[Route("[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IPaginationUtil _paginationUtil;

    public BookController(IBookService bookService, IPaginationUtil paginationUtil)
    {
        _bookService = bookService;
        _paginationUtil = paginationUtil;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery] int page = 1, [FromQuery] int recordsPerPage = 10)
    {
        List<Book> books = await _bookService.GetAllBooksAsync();

        if (books.Count == 0)
            return NotFound();

        var result = _paginationUtil.GetPagination(books, page, recordsPerPage);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        Book book = await _bookService.GetBookById(id);

        if (book.Id == 0)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(BookDTO book)
    {
        Book newBook = new Book{
            Id = 0,
            Author = book.Author,
            Name = book.Name,
            Description = book.Description,
            DateRelase = book.DateRelase,
            IsActived = book.IsActived
        };

        return Ok(await _bookService.CreateBook(newBook));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        if (id != book.Id)
            return BadRequest("Id is different");
        
        var result = await _bookService.UpdateBook(id, book);

        if (result.Id == 0)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook(Book book)
    {
        await _bookService.DeleteBook(book);
        return NoContent();
    }
}