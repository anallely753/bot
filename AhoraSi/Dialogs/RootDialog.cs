using AhoraSi.Commons;
using LumenWorks.Framework.IO.Csv;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace AhoraSi.Dialogs
{
    public class RootDialog: ComponentDialog
    {
        private object stepContext;

        public RootDialog()
        {
            var waterfallStep = new WaterfallStep[]
            {
                InitialStepAsync,
                PreguntaStepAsync,
                ConfirmOptions,
                FinalStepAsync, 
            };




            AddDialog(new WaterfallDialog($"{nameof(RootDialog)}.mainFlow", waterfallStep));
            AddDialog(new ChoicePrompt($"{nameof(ChoicePrompt)}.choice"));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt), ConfirmValidate));

            AddDialog(new GreetingDialog($"{nameof(RootDialog)}.greeting"));
            AddDialog(new nominaDialog($"{nameof(RootDialog)}.nomina"));
            AddDialog(new infonavitDialog($"{nameof(RootDialog)}.infonavit"));
            AddDialog(new vacacionesDialog($"{nameof(RootDialog)}.vacaciones"));
            AddDialog(new cajaAhorroDialog($"{nameof(RootDialog)}.cajaAhorro"));
            AddDialog(new cajaAhorroDialog($"{nameof(RootDialog)}.cajaAhorro"));
            AddDialog(new FormatoDialog($"{nameof(RootDialog)}.formato"));

            InitialDialogId = $"{nameof(RootDialog)}.mainFlow";

        }
        public class Bread
        {
            public static int pos;
            public static int bandera;
        }

        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if(Bread.bandera == 1)
            {
                return await stepContext.NextAsync(stepContext, cancellationToken);
            }
            Bread.bandera = 1;
            return await stepContext.BeginDialogAsync($"{nameof(RootDialog)}.greeting", null, cancellationToken);
        }

        private async Task<DialogTurnResult> PreguntaStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string[] nombre_empleados = {"Pacheco Dorantes Medardo",
"Tzuc Ake Edgar Alfonso",
"Raygoza Rosales Alejandro Javier",
"Escalante Quintal Edgar Mario",
"Campos Uh Jorge Abraham",
"Del Prado Poot Ivan Ezequiel",
"Cetina Gonzalez Ricardo Alejandro",
"Ake Narvaez Juan Gualberto",
"Diaz Echeverria Alejandro De Jesus",
"Pacheco Duarte Alejandro",
"Arana Tun Gabriel Adrian",
"Lizama Hong Miguel Angel",
"Razo Regalado Jorge Enrique",
"Tun Tun Hector",
"Ake Canche Jose Higinio",
"Sanchez Diaz Rene Manuel",
"Plata Canche Rusell",
"Baron Castro Luis Gabriel",
"Hernandez Lugo Luis Roberto",
"Pat Kuk Ivon Marilyn",
"Pareja Cante Jose Emiliano",
"Chan Ku Rolando Alonso",
"Cicero Landon David Alejandro",
"Canul Sulub Jose Freddy",
"Poot Canche Ricardo Concepcion",
"Basto Trejo Gustavo Ariel",
"Loeza Mukul Angel Gualberto",
"Lugo Alvarado Farit Benjamin",
"Chan Huchim Miguel Angel",
"Hernandez Ercila Juan Jesus",
"Gil Flota Alvaro Fernando",
"Ciau Euan Karla Elizabeth",
"Villares Monzon Aida Josefina",
"Ramos Calderon Rafael",
"Chale Perez Angel Miguel",
"Hernandez Pacheco Pedro Inocencio",
"Villegas Ruiz Jorge Adrian",
"Salazar Damian Jesus Antonio",
"Romero Gonzalez Raul Armando",
"Vazquez Pinzon Rafael",
"Chan Bote Jose Guadalupe",
"Lopez Diaz Marco Antonio",
"Uitz Tun Roger Manuel",
"Pech Pech Gabriel De Jesus",
"Villalobos Domenzain Luis Alberto",
"Ortiz Martinez Paolo Fernando",
"Puch Uc Jose Rufino",
"Chable Pacheco Roberto Adolfo",
"Kantun Chan Edwin Fernando",
"Pech Castro Hector Alonzo",
"Cortes Arpay Miguel Adriano",
"Gamboa Fierro Alejandro",
"Palomo Sansores Ricardo Valerio",
"Narvaez Palmero Oscar Ulises",
"Poot Novelo Victor Enrique",
"Mukul Mukul Andres Ariel",
"Salaya Fernandez Beatriz Elena",
"Castro Espinosa Pedro Jose",
"Caamal Fuentes Rosalinda Del Socorro",
"Mendez Malaver Fredy Armando",
"Campos Escamilla Oswaldo Pablo",
"Arenas Gonzalez Marcos Rene",
"Varguez Puch Jorge Antonio",
"Xeque Acereto Ismael David",
"Ek Eb Mauricio",
"Balam Romero Carlos Cesar",
"Chan Moo Jose Manuel",
"Tamay Chan Jose Melchor",
"Baas Ayim Gabriel Eduardo",
"Arjona Leal Ana Laura",
"Pech Rosado Jose Renan",
"Herrera Medina Martin Alfonso",
"Chan Cetina Julian Antonio",
"Chin Poot Flor De Maria",
"Cardeã‘A Carrillo Jose Guadalupe",
"Huchin Acosta Macario",
"Canche Ojeda Ramon Antonio",
"Aviles Chi Rusel Angel",
"Flota Centeno Cristobal Jair",
"Couoh Peã‘A Samuel Israel",
"Mendoza Ake Ernesto Alonso",
"Tec Zuã‘Iga Juan David",
"Garcia Poot Samuel Matias",
"Camara Duarte Ana Patricia",
"Xeque Notario Jorge Livander",
"Cardeã‘A Pool Micky Arcangel",
"Ucan Osorio Alan Moises",
"Magaã‘A Gonzalez Gilberto",
"Navarrete Estrella Jose Francisco",
"Rivera Rodriguez Jesus Miguel",
"Alvarez Navarrete Xareni",
"Plata Campos Patricio Roman",
"0",
"Dueã‘As Montes Pedro",
"May Canche Luis Fernando",
"Herrera Madera Jorge Adalberto",
"Batun Tun Marcos Gregorio",
"Araujo Santos Mario Israel",
"Ramos Marin Raul Alberto",
"Garcia Delgado Noel",
"Valencia Montes Alfonso",
"Avilez Brito Diego Ermilo",
"Rueda Ortega Ricardo",
"Salazar Cordova Nestor Daniel",
"Novelo Zapata Juan Jose",
"Franco Medina Uvenses Rene",
"Mendez Lozano Juan Martin",
"Valle Gonzalez Carlos Alberto",
"De La Cruz Perez Julio Cesar",
"Ocaã‘A Benites Hector Enrique",
"Perez Lopez Rey David",
"Avalos Osorio Sebastian",
"Fuentes Guzman Wuilberth Leonardo",
"0",
"Pelayo Ozuna Gerardo",
"0",
"Rangel Velazquez Juan",
"Garibay Vega Sergio Jesus",
"Ramirez Rivera Norma Alicia",
"Mendez Aguilera Lorena Karina",
"Morales Torres Mardonio",
"Aviles Jimenez Adrian",
"Breã‘A Enriquez Yanet Cecilia",
"Valdes Perez Monica",
"Flores Tejeida Erika Liliana",
"Gallegos Murillo Mario Alberto Tonatihu",
"Cisneros Aguilar Angela",
"Rivera Olvera Marugenia",
"Trejo Sanchez Maria Guadalupe",
"0",
"Ruiz Ruiz Roberto",
"Campos Baas Cesario",
"Gomez Valdez Guadalupe",
"Gonzalez Valencia Jose Antonio",
"Velazquez Escamilla Nestor Gerardo",
"Rios Olivo Manuel",
"Ixmatlahua Ortiz Alberto",
"Olvera Paramo Marcos",
"Miranda Manzano Omar",
"Diaz Trinidad Hector",
"Lopez Ibarra Armando De Jesus",
"Gonzalez Aguilar Octavio",
"Flota Couoh Miguel Alfonso",
"Luna Ortiz Brigadier Boanerges",
"Fuentes Barragan Jose Ricardo",
"Lopez Cruz Jorge Rafael",
"Larios Cruz Javier",
"Merlos Garcia Alejandro Israel",
"Hernandez Fernandez Angel Alejandro",
"Torres Jimenez Eden Eutiquio",
"Toriz Nuã‘Ez Eduardo",
"Bousquet Galvan Carlos Hector",
"Lopez Merino Juan Manuel",
"Ruiz Jimenez Aurelio",
"Garcia Zermeã‘O Jesus Marcos",
"Gomez Lopez Victor Jesus",
"Ronquillo Salva Paul Alexis",
"Tadeo Morales David",
"Perez Mendoza Jesus",
"Gutierrez Manjarrez David Gerardo",
"Ramirez Ramirez Jorge",
"Garcia Flores Abel",
"Arriaga Obregon Moises",
"Cano Flores Pedro",
"Hernandez Ramirez Noe",
"Bobadilla Cortez Fausto",
"Jimenez Trejo Jose Marcos",
"Diaz Calixto Jorge",
"Angeles Ceron Arturo",
"Cordero Navarrete Kevin",
"Ramirez Yllescas Jose Luis",
"Millan Amaro Cristian Ernesto",
"Reyes Olvera Daniel",
"Olguin Mendez Fabian",
"Lopez Sandoval Daul",
"Quintero Monroy Alan",
"Alcantara Diaz Arturo",
"Molar Balderas Javier",
"Hernandez Sanchez Diego Eduardo",
"Garcia Zermeã‘O Abel",
"Ocaã‘A Lopez Angel Olivero",
"Martinez Sanchez Carlos Jesus",
"Nieto Gaspar Leonardo Meliton",
"Garcia Mejia Efren Armando",
"Montoya Bernardo Cristian Esteban",
"Camacho De Jesus Javier",
"Espinosa Ramirez Raul",
"Gutierrez Silva Eduardo Daniel",
"Magaã‘A Perez Luis Angel",
"Zamudio Falcon Carlos",
"Perez Sanchez Erick Norberto",
"Nieto Luna Jose Joaquin",
"Quintero Monroy Jesus",
"Cesareo Quintana Hector",
"0",
"Hernandez Ortiz Ignacio",
"Chan Canche Jose David",
"Moo Caamal Marcos Alejandro",
"Ek Chale Sergio Alfonso",
"Vives Osorio Wilberth Antonio",
"0",
"Tovar Urban Christian",
"Chavez Osnaya Jose Francisco",
"0",
"Gomez Muã‘Oz Jose Antonio",
"Fernandez Burgos Francisco Guadalupe",
"Duran Sanchez Enrique Adolfo",
"Ramirez Lopez Gustavo Santiago",
"De Los Santos Gallegos Roman",
"Lopez Cobos Diego Armando",
"Diaz Dominguez Santiago",
"Diaz Zapata Javier",
"Vazquez Hernandez Jose Alberto",
"Rodas Castillo Jesus Manuel",
"Castaã‘Eda Sanchez Arquimedes",
"Perez Montejo Julio Cesar",
"Jimenez Hernandez Erick Alberto",
"Rodriguez Madrigal Erick Omar",
"Torres Velazquez Eriberto",
"Sanchez Trujillo Gabriel",
"Rueda Lopez Misael",
"Ramirez Santiago Gustavo",
"Izcapa Lopez Jesus Armando",
"Torres Garcia Walter",
"Almeida Lopez Lazaro",
"Aguilar Hernandez Jose Luis",
"Primo Brindis Herman Enrique",
"Madrigal Lopez Baruc",
"Frias Olan Miguel Angel",
"Jimenez Cruz Vicente",
"Osorio Magaã‘A Jonas",
"Zapata Rios Francisco",
"Ramos Hernandez Daniel",
"Mena Salaya Juan Jesus",
"Gonzalez Castellanos Elizabeth",
"Perez Gomez Cesar Alonso",
"Sanchez Hernandez Joselito",
"De Dios Bautista Juan Jose",
"Garcia Lopez Carlos Arturo",
"Jeronimo Oliva Alejandro",
"Hernandez Mendez Francisco",
"Lopez Torres Julian",
"Manzano Rodriguez Manuel",
"Guzman Hernandez Francisco Javier",
"De Dios De La Cruz Candelario",
"Garcia Albert Abenamar",
"Izquierdo Gonzalez Pablo",
"Hernandez Perez Jose",
"Gutierrez Primo Alejandro",
"Morales Cuevas Ricardo Concepcion",
"Leon Murillo Eden",
"Naranjo Jimenez Raul Antonio",
"0",
"Jaimes Gonzalez Eber Abner",
"Martinez Barrera Sandra Noemi",
"Pineda Gonzalez Juan Antonio",
"Benitez Prisciliano Raul",
"Hernandez Rosales Jose Fernando",
"Valle Baldomero Carlos Alberto",
"Belman Alvarez Daniela Yuritzi",
"Flores Santiago Maribel Alejandra",
"Herrera Vargas Dafne Kassandra",
"Valdovinos Hernandez Blanca Esthela",
"Mandujano Perez Joel Fernando",
"Flores Gonzalez Jessica Grisel",
"0",
"Aguilar Magaã‘A Pedro Alberto",
"Perez Javier Mara Elizabeth",
"Canche Dzul Roger Salvador",
"Valencia Valencia Erick",
"Arevalo Jimenez Jorge",
"Lara Pardo Jaime",
"Toto Arias Cristian Antonio",
"Lara Lopez Laura",
"Escalon Gamas Jacqueline",
"Cortazar Almeida Jesus Alberto",
"0",
"Euan Capistran Alvaro Andres",
"Tut Herrera Armin Eduardo",
"Ramirez Flores Jose Nicolas",
"Ku Amaya Joaquin Ramily",
"Tun Pech Carlos Enrique",
"Flores Leon Geronimo De Jesus",
"Garcia Xool Luis Manuel",
"Pavia Castillo Jonatan De Jesus",
"Perera Che Yesica Eugenia",
"Medina Gonzalez Erik",
"Vela Valdez Francisco Javier",
"Canul Balam Leonel",
"Silvano Hernandez Victor Manuel",
"Tzab Dzib Marco Antonio",
"Peã‘A Lopez Jose Antonio",
"Panti Chan Jesus Fernando Benjamin",
"Amaya Puga Jorge Julio",
"Cocom Chuc Samuel",
"Aguilar Ek Melisa Ivette",
"Rodriguez Canul Pedro Damian",
"Escamilla Rodriguez Angel Antonio",
"Alba Rico Alejandro",
"Gil May Luis Gabriel",
"Gomez Sanchez Jose",
"Sosa Chan Julio Cesar",
"Be Uicab Lorenzo Manuel",
"Pech Perez Ivana Del Jesus",
"Duarte Pech Jose Adrian",
"Zapata Peralta Sergio De Jesus",
"Uicab Vera Angel David",
"Zetina Uc Jose De La Cruz",
"Peraza Fernandez Juan Carlos",
"Leon Rodriguez Cruz Alejandro",
"Cardenas Cruz Ruben Ismael",
"0",
"Calderon Sabido Alberto Jesus",
"Flores Rivera Alejandra",
"Sulub Chan Jose Dani Damian",
"Carvente Torres Oscar Jonathan",
"Perez Molina Luis Francisco",
"Ojeda Barrios Ramon",
"Avalos Vargas Luis Antonio",
"Gomez Hernandez Adriana",
"Hoyos Brito Luis Humberto",
"Saume Moreno Marco Antonio",
"Cervantes Mayo Andrea Estefania",
"Lopez Alvarado Mariela",
"Olmos Mayorga Ivan Enrique",
"0",
"Martinez Castro Raul",
"Cruz Cortes Andres",
"Medina Velazquez Rigoberto",
"Morales Gonzalez Victor Ciro",
"Martinez Salgado Miguel Angel",
"Juarez Rodriguez Jose Aaron",
"Lozano De La Rosa Juan Carlos",
"Valdes Escobedo Daniel",
"Juarez Rodriguez Angel",
"Gallardo Tapia Jose Antonio",
"Mendieta Delgado Jose Antonio",
"Cordova Parra Jose Ricardo",
"Soto Moreno Miguel Enrique",
"Gutierrez Oblea Arturo",
"Jimenez Leal Andres",
"Ruiz Montes De Oca Bernardo",
"Rayon Raymundo Arturo Aron",
"Correa Perez Gerardo",
"Victoria Valencia Hugo",
"Robles Martinez Oscar",
"De Rita Chipule Emma",
"Garcia Avilez Angel",
"Gonzalez Valero Carlos Alberto",
"Marquez Perez Jose Francisco",
"Martinez Ramon Rafael",
"Garcia Perez Jose Constantino",
"Lorenzo Flores Claudia Ivette",
"Huerta Ramirez Edgar Uriel",
"Siguenza Rosales Eduardo",
"Ruiz Ramirez Ricardo",
"Acevedo Acevedo Juan Carlos",
"Gaytan Diaz Jose David",
"Becerril Fraga Carlos Alberto",
"Santos Trinidad Fidel",
"Lopez Guzman Salvador",
"Ceballos Pardo Enrique Antonio",
"Nuã‘Ez Sanchez Oscar Javier",
"Cruz Ramos German",
"Bermudez Chavez Angel",
"Sanchez Cervantes Eric",
"Moran Ireta Enrique",
"Manzano Sanchez Ivan",
"Marquez Cruz Armando",
"0",
"May Herrera Rosa Alejandrina",
"Ramos Sansores Carlos Alberto",
"Mis Pavon Juan Jose",
"Canche Mex Leonardo Raymundo",
"Gutierrez Posada Juan Raul",
"Cortes Rodriguez Jose Luis",
"Hoy Ceh Jose Cristobal",
"Valle Uvalle Jesus Bernardino",
"0",
"Martinez Araujo Cecilio Miguel",
"Aguilar Lopez Hector Luciano",
"0",
"Chan Ponce Daniel Alberto",
"Moreno Laguna Jorge Armando",
"Campos Alcantar Cristal De Jesus",
"Ramirez Muã‘Oz Luzmar",
"Sanchez Zuã‘Iga Pablo Sergio",
"Cel Ake Oscar Alberto",
"0",
"Gutierrez Bracamonte Carlos",
"0",
"Sanchez Hidalgo Alan",
"0",
"De Jesus Martinez Escolastica"
};
            string nombre = nombre_empleados[Bread.pos];
            if (Bread.bandera != 1)
            {
                await stepContext.Context.SendActivityAsync($"Hola {nombre}", cancellationToken: cancellationToken);
            }
            
            return await OptionButton.ShowOptions(stepContext, cancellationToken);
        }

        private async Task<DialogTurnResult> ConfirmOptions(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var option = stepContext.Context.Activity.Text;
            switch (option)
            {
                case "Nómina":
                    return await stepContext.BeginDialogAsync($"{nameof(RootDialog)}.nomina", null, cancellationToken);
                case "Infonavit":
                    return await stepContext.BeginDialogAsync($"{nameof(RootDialog)}.infonavit", null, cancellationToken);
                case "Vacaciones":
                    return await stepContext.BeginDialogAsync($"{nameof(RootDialog)}.vacaciones", null, cancellationToken);
                case "Caja de ahorro":
                    return await stepContext.BeginDialogAsync($"{nameof(RootDialog)}.cajaAhorro", null, cancellationToken);
                case "Beneplus":
                    return await stepContext.BeginDialogAsync($"{nameof(RootDialog)}.beneplus", null, cancellationToken);
                case "Formatos":
                    return await stepContext.BeginDialogAsync($"{nameof(RootDialog)}.formato", null, cancellationToken);
                default:
                    await stepContext.Context.SendActivityAsync("Opción no valida", cancellationToken: cancellationToken);
                    break;
            }
            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<bool> ConfirmValidate(PromptValidatorContext<bool> promptContext, CancellationToken cancellationToken)
        {
            var option = promptContext.Recognized.Value;
            if (option)
                await promptContext.Context.SendActivityAsync("Ok", cancellationToken: cancellationToken);
            else
                await promptContext.Context.SendActivityAsync("¡Hasta pronto!", cancellationToken: cancellationToken);
            return true;
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
           return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

    }
}
