using ModuloLINQ;

Console.WriteLine("----Modulo de LINQ----");

//int[] numeros = { 1, 2, 3, 4, 5 };
//creando arreglo de manera "sencilla"
int[] numeros = Enumerable.Range(1, 20).ToArray();

//Esto es LINQ (where, sintaxis de metodos)
//var numerosPares = numeros.Where(n => n % 2 == 0).ToList();
var numerosPares = numeros.Where(n =>
{
    Console.WriteLine($"Evaluando si {n} es par");
    return n % 2 == 0;
});

//forma 2: sintaxis de queries
//var numerosPares2 = (from n in numeros
//where n % 2 == 0
//select n).ToList();

Console.WriteLine();

foreach (var numero in numerosPares)
{
    Console.WriteLine($"el numero {numero} es par");
}

Console.WriteLine();


//filtrando con la sentencia where 


//var numerosImpares = numeros.Where(n => n % 2 == 1).ToList();

//var numerosImpares = numeros.Where(n =>
//{ 
//    Console.WriteLine($"Evaluando si {n} es impar ");
//    return n % 2 == 1;
//});

//foreach (var nImpar in numerosImpares)
//{
//    Console.WriteLine($"El numero {nImpar} es Impar");
//}
Console.WriteLine();

var numerosImpares = numeros.Where(n => n % 2 == 1 && n > 10).ToList();



//Ejemplo 2: Coleccion de objetos

var personas = new List<Persona>()
{
    new Persona { Nombre = "Eduardo", Edad = 30, FechaIngresoALaEmpresa = new DateTime(2021, 1, 2), Soltero = true },
    new Persona { Nombre = "Nidia", Edad = 19, FechaIngresoALaEmpresa = new DateTime(2015, 11, 22), Soltero = true },
    new Persona { Nombre = "Alejandro", Edad = 45, FechaIngresoALaEmpresa = new DateTime(2020, 4, 12), Soltero = false },
    new Persona { Nombre = "Valentina", Edad = 24, FechaIngresoALaEmpresa = new DateTime(2021, 7, 8), Soltero = false },
    new Persona { Nombre = "Roberto", Edad = 61, FechaIngresoALaEmpresa = DateTime.Now.AddDays(-1), Soltero = false },
};

var personasDe25AñosOMenos = personas.Where(p => p.Edad <= 25).ToList();

foreach (var persona in personasDe25AñosOMenos)
{
    Console.WriteLine($"{persona.Nombre} tiene {persona.Edad} años.");
}

Console.WriteLine();

var solteros = personas.Where(p => p.Soltero).ToList();

foreach (var soltero in solteros)
{
    Console.WriteLine($"¿{soltero.Nombre} es solter@? {(soltero.Soltero ? "sí" : "no")}");
}

Console.WriteLine();

var solterosMenoresDe25 = personas.Where(p => p.Soltero && p.Edad <= 25).ToList();

foreach (var persona in solterosMenoresDe25)
{
    Console.WriteLine($"{persona.Nombre} es soltera y es menor de 25");
}

Console.WriteLine();

var fechaActual = DateTime.Today;
//Console.WriteLine(fechaActual.AddMonths(-3).ToString());
var personasConMenosDe3MesesEnLaEmpresa = personas.Where(p => p.FechaIngresoALaEmpresa >= fechaActual.AddMonths(-3) ).ToList();
foreach (var persona in personasConMenosDe3MesesEnLaEmpresa)
{
    Console.WriteLine($"{persona.Nombre} tiene menos de 3 meses en la empresa");
}

Console.WriteLine();


//first o fisrtOrDefault

var primeraPersonaMenorDe25 = personas.First(p => p.Edad < 25);
var primeraPersonaMayorDe100 = personas.FirstOrDefault(p => p.Edad > 100);

if (primeraPersonaMayorDe100 is null)
{
    Console.WriteLine("No hay ninguna persona mayor de 100");
}

//combinar funciones de LINQ
var primeraPersonaMayorDe60 = personas.Where(p => p.Edad > 60).FirstOrDefault();

Console.WriteLine();


//ORDER BY
//Ejemplo 1: Ordenando por edad de manera ascendente
foreach (var persona in personas.OrderBy(p => p.Edad))
{
    Console.WriteLine($"{persona.Nombre} - {persona.Edad}");
}

Console.WriteLine();

//Ejemplo 2: Ordenando por edad de manera descendente
foreach (var persona in personas.OrderByDescending(p => p.Edad))
{
    Console.WriteLine($"{persona.Nombre} - {persona.Edad}");
}

Console.WriteLine();

//Ejemplo 3: Ordenando numeros
int[] numeros2 = { 1, 5, 12, 3, 111, 6 };

var numerosOrdenados = numeros2.OrderBy(x => x).ToList();
foreach (var numerosss in numerosOrdenados)
{
    Console.WriteLine(numerosss);
}

Console.WriteLine();


//SELECT

//Ejemplo 1: Seleccionar una propiedad
var nombres = personas.Select(p => p.Nombre).ToList();

//Ejemplo 2: Seleccionar varias propiedades
var nombresYEdades = personas.Select(p => new { Nombre = p.Nombre, Edad = p.Edad }).ToList();

//Ejemplo 3: Proyectar hacia una clase
var personaDTO = personas.Select(p => new PersonaDTO {Nombre = p.Nombre, Edad = p.Edad }).ToList();

//Ejemplo 4; Realizar una operacion
var numreos3 = Enumerable.Range(1, 5).ToList();
var numerosDuplicado = numeros.Select(n => n * 2).ToList();

//Ejemplo 5: Tomar el indice
var nombresConOrden = personas.Select((persona, indice) => new { Nombre = persona.Nombre, Orden = indice + 1 } ).ToList();
foreach (var item in nombresConOrden)
{
    Console.WriteLine($"{item.Orden}) {item.Nombre}");
}

Console.WriteLine();


//SelectMany

var pers0nas = new List<Persona>() {
new Persona { Nombre = "Eduardo", Telefonos = { "123-456", "789-852" } },
new Persona { Nombre = "Nidia", Telefonos = { "998-478", "568-222" } },
new Persona { Nombre = "Alejandro", Telefonos = { "712-132" } },
new Persona { Nombre = "Valentina" }
};

//Ejemplo 1: Seleccionar telefonos sin personas
var telefonos = pers0nas.SelectMany(p => p.Telefonos).ToList();

//Ejemplo 2: Entendiendo SelectMany con dos seleccionaes diferentes
int[] numer0s = { 1, 2, 3 };
var personasYNumeros = pers0nas.SelectMany(p => numer0s, (persona, numero) => new
{
    Persona = persona,
    Numero = numero
});

foreach (var item in personasYNumeros)
{
    Console.WriteLine($"{item.Persona.Nombre} - {item.Numero}");
}

//Ejemplo 3: Personas y telefonos
var personasYTelefonos = pers0nas.SelectMany(p => p.Telefonos, (persona, telefono) => new
{
    Persona = persona,
    Telefono = telefono
});
foreach (var item in personasYTelefonos)
{
    Console.WriteLine($"{item.Persona.Nombre} - {item.Telefono}");
}



//Funciones escalares: COUNT, SUM. MIN, MAX, AVG
var pers0nass = new List<Persona>() {
new Persona { Nombre = "Eduardo", Soltero = true, Edad = 19 },
new Persona { Nombre = "Nidia", Soltero = true, Edad = 25 },
new Persona { Nombre = "Alejandro", Soltero = true, Edad = 30 },
new Persona { Nombre = "Valentina", Soltero = false, Edad = 22 }
};


//Ejemplo 1: COUNT
Console.WriteLine($"La cantidad de personas es: {pers0nass.Count()}");
Console.WriteLine($"La cantidad de personas es: {pers0nass.Count(p => p.Soltero)}");
Console.WriteLine($"La cantidad de personas es: {pers0nass.LongCount()}");

//Ejemplo 2: SUM
var numeross = Enumerable.Range(1, 5);
var suma = numeross.Sum();
var sumaEdades = pers0nass.Sum(p => p.Edad);

//Ejemplo 3: MIN
var minimo = numeross.Min();
var edadMinima = pers0nass.Min(p => p.Edad);
var personaConLaEdadMasPequeña = pers0nass.MinBy(p => p.Edad);

//Ejemplo 4: MAX
var maximo = numeross.Max();
var edadMaxiam = pers0nass.Max(p => p.Edad);
var persoaConLaEdadMasGrande = pers0nass.MaxBy(p => p.Edad);

//Ejemplo 5: AVG
var promedio = numeross.Average();
var promedioEdades = pers0nass.Average(p => p.Edad);



//Cuantificadores Universales: All, Any, Contains

//Ejemplo 1: All = ¿Son todos, todos tiene, etc?
var sonTodasLasPersonasMayoresDeEdad = pers0nass.All(p => p.Edad >= 18);
var sonTodosSolteros = pers0nass.All(p => p.Soltero);


//Ejemplo 2: Any ¿alguno, existe, etc?
var existeMenor = pers0nass.Any(p => p.Edad < 18);
var existePersonaMayorDe35 = pers0nass.Any(p => p.Edad > 35);

//Ejemplo 3: Contains ¿está, pertenece, es miembro, etc?
var numerossssssss = Enumerable.Range(1, 100);
var estaElNumero3 = numerossssssss.Contains(3);
var estaElNumero20 = numerossssssss.Contains(300);



//TAKE Y SKIP

//take
var primeros10Numeros = numerossssssss.Take(10).ToList();
var ultimos10Numeros = numerossssssss.TakeLast(10).ToList();

//skip
var segundoLoteDe10 = numerossssssss.Skip(10).Take(10).ToList();
var segundoLoteUltimos10 = numerossssssss.SkipLast(10).TakeLast(10).ToList();


//GroupBy

var personasGby = new List<Persona>() {
    new Persona { Nombre = "Eduardo",Edad = 19, Soltero = true },
    new Persona { Nombre = "Nidia", Edad = 25, Soltero = true },
    new Persona { Nombre = "Alejandro", Edad = 30, Soltero = true },
    new Persona { Nombre = "Valentina", Edad = 17, Soltero = false },
    new Persona { Nombre = "Roberto", Edad = 18, Soltero = true },
    new Persona { Nombre = "Eugenia", Edad = 27, Soltero = false },
    new Persona { Nombre = "Esmerlin", Edad = 45, Soltero = false }
};

var agrupamientoPorSolteria = personasGby.GroupBy(p => p.Soltero);
foreach ( var grupo in agrupamientoPorSolteria)
{
    Console.WriteLine($"Grupo de las personas donde soltero = {grupo.Key} (Total: {grupo.Count()})");
    foreach (var persona in grupo)
    {
        Console.WriteLine($"- {persona.Nombre}");
    }
}


//Distinct y DistinctBy

var personasDis = new List<Persona>() {
new Persona { Nombre = "Eduardo", EmpresaId = 1, },
new Persona { Nombre = "Nidia",  EmpresaId = 1 },
new Persona { Nombre = "Eduardo"},
new Persona { Nombre = "Esmerlin", EmpresaId = 3 }
};

int[] numerosDis = { 1, 2, 3, 1, 1, 6 };

var numerosSinRepeticion = numerosDis.Distinct();

var personasSinRepeticion = personasDis.DistinctBy(p => p.Nombre);


//CHUNK
int[] ch = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var resultado = ch.Chunk(3);












Console.WriteLine();