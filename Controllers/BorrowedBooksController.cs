
using DTO;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Controllers;

[Route("[controller]")]
[ApiController]
public class BorrowedBooksController : ControllerBase
{
    private readonly ICommonService<BorrowedBooks> _borrowedBooksService;
    private readonly ICommonService<User> _userService;
    private readonly IBookService _bookService;
    private readonly IPaginationUtil _paginationUtil;

    public BorrowedBooksController(
        [FromKeyedServices("BorrowedBooksService")] ICommonService<BorrowedBooks> borrowedBooksService,
        [FromKeyedServices("UserService")] ICommonService<User> userService,
        IBookService bookService,
        IPaginationUtil paginationUtil
    )
    {
        _borrowedBooksService = borrowedBooksService;
        _userService = userService;
        _bookService = bookService;
        _paginationUtil = paginationUtil;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBorrowedBooks([FromQuery] int page = 1, [FromQuery] int recordsPerPage = 10)
    {
        List<BorrowedBooks> borrowedBooks = await _borrowedBooksService.GetAllAsync();

        if (borrowedBooks.Count == 0)
            return NotFound();

        var result = _paginationUtil.GetPagination(borrowedBooks, page, recordsPerPage);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBorrowedBookById(int id)
    {
        BorrowedBooks borrowedBook = await _borrowedBooksService.GetByIdAsync(id);

        if (borrowedBook.Id == 0)
            return NotFound();

        return Ok(borrowedBook);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBorrowedBook(BorrowedBooksDTO borrowedBook)
    {
        User user = await _userService.GetByIdAsync(borrowedBook.UserId);
        List<Book> books = await _bookService.GetAllBooksByIds(borrowedBook.BookIds!);

        BorrowedBooks newBorrowedBook = new BorrowedBooks{
            Id = 0,
            User = user,
            Books = books,
            DateBorrowed = borrowedBook.DateBorrowed,
            ReturnDate = borrowedBook.ReturnDate
        };

        await _borrowedBooksService.CreateAsync(newBorrowedBook);

        if (newBorrowedBook.Id == 0)
            return NotFound();

        return Ok(newBorrowedBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBorrowedBook(int id, BorrowedBooks borrowedBook)
    {
        if (id != borrowedBook.Id)
            return BadRequest("Id is different");

        BorrowedBooks result = await _borrowedBooksService.UpdateAsync(id, borrowedBook);

        if (result.Id == 0)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBorrowedBook(BorrowedBooks borrowedBook)
    {
        await _borrowedBooksService.DeleteAsync(borrowedBook);
        return NoContent();
    }
}