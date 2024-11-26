using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DB;
using WebApp.Models.Entities;


namespace WebApp.Controllers
{
    [Route("api/users")]//това е атрибут, който указва, че всички методи в този контролер ще отговарят на HTTP заявки, изпратени до /api/users.
    [ApiController]//Този атрибут обозначава, че контролерът е API контролер. Това предоставя допълнителни функции, като автоматично управление на грешки и валидация на входни данни.
    public class UsersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Конструкторът приема контекста на базата данни чрез Dependency Injection
        //Той се извиква всеки път, когато контролерът се създава.
        public UsersApiController(ApplicationDbContext context) 
        {
            _context = context;
        }

        // Метод за получаване на всички потребители от базата данни
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            //ACTIONRESULT:
            //ActionResult е тип, който представлява резултат от действие в контролер на ASP.NET. Той може да връща различни HTTP статус кодове и
            //типове отговори, в зависимост от ситуацията. 
            //Когато връщате Ok(), методът автоматично задава HTTP статус код 200 (OK), което показва, че заявката е успешна.
            //В ActionResult<T> типът <T> указва типа на данните, които ще се върнат в отговора. В примера, ActionResult<IEnumerable<User>> показва,
            // че ще се върне списък от потребители.

            // Извличаме всички потребители от базата данни асинхронно
            var users = await _context.Users.ToListAsync();

            //ToListAsync():
            //Това е асинхронен метод, предоставен от Entity Framework.
            //Той извиква базата данни и извлича всички записи от таблицата Users, след което ги конвертира в списък (List<User>).
            //ToListAsync() работи асинхронно, което означава, че методът няма да блокира основния поток на приложението, докато данните се извличат.
            //Вместо това, той ще позволи на приложението да обработва други операции, докато изчаква отговор от базата данни.

            //await:Ключовата дума await се използва за изчакване на завършването на асинхронната операция. Когато се достигне await,
            //контролът се връща на основния поток, за да може приложението да продължи с обработката на други задачи.
            //Когато ToListAsync() завърши и данните са извлечени, контролът отново се предава на текущия метод, а 
             //резултатът(списък с потребители) се присвоява на променливата users.


            // Връщаме списъка с потребители (HTTP 200 OK)
            return Ok(users);
        }

        // Метод за добавяне на нов потребител
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            if (user == null)
            {
                // Връща статус 400 Bad Request, ако потребителят е null
                return BadRequest();
            }

            // Добавяме новия потребител в базата данни
            _context.Users.Add(user);
            // Записваме промените асинхронно
            await _context.SaveChangesAsync();

            // Връщаме статус 201 Created и информацията за новосъздадения потребител
            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
        }

        [HttpGet("CheckUsernameExist")]
        public async Task<ActionResult<bool>> CheckUsernameExist(string username)
        {
            var usernameExist = await _context.Users
                .AnyAsync(user => user.Name == username);

            return Ok(usernameExist);
        }

        [HttpGet("CheckEmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var emailExist = await _context.Users
                .AnyAsync(user => user.Email == email);

            return Ok(emailExist);
        }

    }
}




/*namespace WebApp.Controllers
{
    [Route("api/users")]// Определя маршрута на контролера
    [ApiController] // Декорира контролера като API контролер ( Декорира контролера с атрибут,
                    // който предоставя функционалности, специфични за API, като автоматично моделиране на грешки и обработка на параметри.)
    public class UsersApiController : Controller //Наследяване от ControllerBase, за да създадем контролер за API
    {
        private static List<User> Users = new List<User>(); //Списък за съхранение на потребители в паметта

        [HttpGet] // Декорира метода с атрибут, който указва, че той отговаря на GET заявка
        public ActionResult<IEnumerable<User>> GetAllUsers() {
            //IEnumerable<T> е интерфейс, който дефинира метод за итериране (обхождане) на колекция от елементи от тип T

             //ActionResult е тип, който представлява резултат от действие в контролер на ASP.NET. Той може да връща различни HTTP статус кодове и
           //типове отговори, в зависимост от ситуацията. 
            //Когато връщате Ok(), методът автоматично задава HTTP статус код 200 (OK), което показва, че заявката е успешна.
            //В ActionResult<T> типът <T> указва типа на данните, които ще се върнат в отговора. В примера, ActionResult<IEnumerable<User>> показва,
           // че ще се върне списък от потребители.
            

            return Ok(Users); //Връща HTTP отговор със статус 200 (OK) и съдържание, което е списъкът с потребители.
                              //Методът Ok() автоматично форматира данните в JSON.
        }

        [HttpPost] // Декорира метода с атрибут, който указва, че той отговаря на POST(създаване) заявка.
        public ActionResult<User> AddUser([FromBody]User user)
        {
            if( user == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);//това ид е за url например Location: api/users/1
            //Методът CreatedAtAction е част от класовете, свързани с ASP.NET MVC и се използва за генериране на HTTP отговор, който индикира,
            //че нов ресурс е създаден успешно.Той също така предоставя информация за местоположението на новосъздадения ресурс.
            //CreatedAtAction а този метод приеме името на екшъна,ид , и обекта 
        }
    }
}
*/
