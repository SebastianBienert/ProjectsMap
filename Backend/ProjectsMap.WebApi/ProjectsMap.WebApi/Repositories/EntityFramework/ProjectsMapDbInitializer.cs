using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.EntityFramework
{
    public class ProjectsMapDbInitializer : DropCreateDatabaseAlways<EfDbContext>
    {
        protected override void Seed(EfDbContext context)
        {
           // base.Seed(context);
            IList<Vertex> vertices = new List<Vertex>()
            {
                new Vertex(0,0),
                new Vertex(80,20),//seat free
                new Vertex(70,70),
                new Vertex(0,70),
                new Vertex(20,20),//seat
                new Vertex(50,20),//seat
                new Vertex(140,70),
                new Vertex(140,0), //7
                new Vertex(210,70),
                new Vertex(210,0), //9
                new Vertex(280,70),
                new Vertex(280,0), //11
                new Vertex(350,70),
                new Vertex(350,0), //13
                new Vertex(420,70),
                new Vertex(420,0), //15
                new Vertex(160,20),//seat free
                new Vertex(180,20),//seat free
                new Vertex(490,70),
                new Vertex(230,20), //19 seat free
                new Vertex(560,70),
                new Vertex(560,0), //21
                new Vertex(0,210),
                new Vertex(70,210), //23
                new Vertex(490,190),
                new Vertex(560,190), //25
                new Vertex(70,350),
                new Vertex(0,350), //27
                new Vertex(490,230),
                new Vertex(560,230), //29
                new Vertex(560,350),
                new Vertex(490,350), //31
                new Vertex(0,510),
                new Vertex(140,510), //33
                new Vertex(140,440),
                new Vertex(70,440), //35
                new Vertex(210,440),
                new Vertex(210,510), //37
                new Vertex(280,440),
                new Vertex(280,510), //39
                new Vertex(350,440),
                new Vertex(350,510), //41
                new Vertex(420,440),
                new Vertex(420,510), //43
                new Vertex(560,510),
                new Vertex(560,351), //45                                           //NOT USED
                new Vertex(490,351),                                                //NOT USED
                new Vertex(490,440), //47
                new Vertex(100,100),
                new Vertex(280,100), //49
                new Vertex(280,210),
                new Vertex(100,210), //51
                new Vertex(450,100),
                new Vertex(450,210), //53
                new Vertex(210,240),
                new Vertex(210,410), //55
                new Vertex(100,410),
                new Vertex(340,240), //57
                new Vertex(340,410),
                new Vertex(450,410), //59
                new Vertex(450,240),
                new Vertex(100,240), // 61
                new Vertex(250,20), // seat free
                new Vertex(300,20), // 63 seat free
                new Vertex(320,20), // seat free
                new Vertex(370,20), // 65 seat free
                new Vertex(390,20), // seat free
                new Vertex(440,20), // 67 seat free
                new Vertex(470,20), // seat free
                new Vertex(500,20), // 69 seat free
                new Vertex(530,20), // seat free
                new Vertex(110,20), // 71 seat free
                new Vertex(130,120), // seat free
                new Vertex(170,120), // 73 seat free
                new Vertex(210,120), // seat free
                new Vertex(250,120), // 75 seat free
                new Vertex(130,180), // seat free
                new Vertex(170,180), // 77 seat free
                new Vertex(210,180), // seat free
                new Vertex(250,180), // 79 seat free
                new Vertex(300,120), // seat free
                new Vertex(340,120), // 81 seat free
                new Vertex(380,120), // seat free
                new Vertex(420,120), // 83 seat free
                new Vertex(300,180), // seat free
                new Vertex(340,180), // 85 seat free
                new Vertex(380,180), // seat free
                new Vertex(420,180), // 87 seat free
                new Vertex(20,100), // seat free
                new Vertex(20,170), // 89 seat free
                new Vertex(20,240), // seat free
                new Vertex(20,310), // 91 seat free
                new Vertex(160,480), // seat free
                new Vertex(230,480), // 93 seat free
                new Vertex(300,480), // seat free
                new Vertex(370,480), // 95 seat free
                new Vertex(10,370), // seat free
                new Vertex(10,400), // 97 seat free
                new Vertex(50,370), // seat free
                new Vertex(50,400), // 99 seat free
                new Vertex(20,480), // seat free
                new Vertex(50,480), // 101 seat free
                new Vertex(80,480), // seat free
                new Vertex(110,480), // 103 seat free
                new Vertex(500,370), // seat free
                new Vertex(500,400), // 105 seat free
                new Vertex(540,370), // seat free
                new Vertex(540,400), // 107 seat free
                new Vertex(440,480), // seat free
                new Vertex(470,480), // 109 seat free
                new Vertex(500,480), // seat free
                new Vertex(530,480), // 111 seat free
            };

            IList<Room> rooms = new List<Room>()
            {
                new Room()
                {
                    RoomId = 1,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[3].X,
                            StartVertexY = vertices[3].Y,
                            EndVertexX = vertices[6].X,
                            EndVertexY = vertices[6].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[0].X,
                            StartVertexY = vertices[0].Y,
                            EndVertexX = vertices[3].X,
                            EndVertexY = vertices[3].Y
                        },

                        new Wall()
                        {
                            StartVertexX = vertices[6].X,
                            StartVertexY = vertices[6].Y,
                            EndVertexX = vertices[7].X,
                            EndVertexY = vertices[7].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[7].X,
                            StartVertexY = vertices[7].Y,
                            EndVertexX = vertices[0].X,
                            EndVertexY = vertices[0].Y
                        },
                    }
                    //new List<Vertex>() {vertices[0],vertices[3],vertices[6],vertices[7]}
                },
                new Room()
                {
                    RoomId = 2,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[48].X,
                            StartVertexY = vertices[48].Y,
                            EndVertexX = vertices[49].X,
                            EndVertexY = vertices[49].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[49].X,
                            StartVertexY = vertices[49].Y,
                            EndVertexX = vertices[50].X,
                            EndVertexY = vertices[50].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[50].X,
                            StartVertexY = vertices[50].Y,
                            EndVertexX = vertices[51].X,
                            EndVertexY = vertices[51].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[51].X,
                            StartVertexY = vertices[51].Y,
                            EndVertexX = vertices[48].X,
                            EndVertexY = vertices[48].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[48],vertices[49],vertices[50],vertices[51]}
                },
                new Room()
                {
                    RoomId = 3,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[6].X,
                            StartVertexY = vertices[6].Y,
                            EndVertexX = vertices[7].X,
                            EndVertexY = vertices[7].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[7].X,
                            StartVertexY = vertices[7].Y,
                            EndVertexX = vertices[9].X,
                            EndVertexY = vertices[9].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[9].X,
                            StartVertexY = vertices[9].Y,
                            EndVertexX = vertices[8].X,
                            EndVertexY = vertices[8].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[8].X,
                            StartVertexY = vertices[8].Y,
                            EndVertexX = vertices[6].X,
                            EndVertexY = vertices[6].Y
                        },
                    }
                   // Vertexes = new List<Vertex>() {vertices[6],vertices[7],vertices[9],vertices[8]}
                },
                new Room()
                {
                    RoomId = 4,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[9].X,
                            StartVertexY = vertices[9].Y,
                            EndVertexX = vertices[8].X,
                            EndVertexY = vertices[8].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[8].X,
                            StartVertexY = vertices[8].Y,
                            EndVertexX = vertices[10].X,
                            EndVertexY = vertices[10].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[10].X,
                            StartVertexY = vertices[10].Y,
                            EndVertexX = vertices[11].X,
                            EndVertexY = vertices[11].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[11].X,
                            StartVertexY = vertices[11].Y,
                            EndVertexX = vertices[9].X,
                            EndVertexY = vertices[9].Y
                        },
                    },
                    //Vertexes = new List<Vertex>() {vertices[9],vertices[8],vertices[10],vertices[11]}
                },
                new Room()
                {
                    RoomId = 5,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[11].X,
                            StartVertexY = vertices[11].Y,
                            EndVertexX = vertices[10].X,
                            EndVertexY = vertices[10].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[10].X,
                            StartVertexY = vertices[10].Y,
                            EndVertexX = vertices[12].X,
                            EndVertexY = vertices[12].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[12].X,
                            StartVertexY = vertices[12].Y,
                            EndVertexX = vertices[13].X,
                            EndVertexY = vertices[13].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[13].X,
                            StartVertexY = vertices[13].Y,
                            EndVertexX = vertices[11].X,
                            EndVertexY = vertices[11].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[11],vertices[10],vertices[13],vertices[12]}
                },
                new Room()
                {
                    RoomId = 6,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[13].X,
                            StartVertexY = vertices[13].Y,
                            EndVertexX = vertices[12].X,
                            EndVertexY = vertices[12].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[12].X,
                            StartVertexY = vertices[12].Y,
                            EndVertexX = vertices[14].X,
                            EndVertexY = vertices[14].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[14].X,
                            StartVertexY = vertices[14].Y,
                            EndVertexX = vertices[15].X,
                            EndVertexY = vertices[15].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[15].X,
                            StartVertexY = vertices[15].Y,
                            EndVertexX = vertices[13].X,
                            EndVertexY = vertices[13].Y
                        },
                    }
                   // Vertexes = new List<Vertex>() {vertices[13],vertices[12],vertices[14],vertices[15]}
                },
                new Room()
                {
                    RoomId = 7,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[15].X,
                            StartVertexY = vertices[15].Y,
                            EndVertexX = vertices[14].X,
                            EndVertexY = vertices[14].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[14].X,
                            StartVertexY = vertices[14].Y,
                            EndVertexX = vertices[20].X,
                            EndVertexY = vertices[20].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[20].X,
                            StartVertexY = vertices[20].Y,
                            EndVertexX = vertices[21].X,
                            EndVertexY = vertices[21].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[21].X,
                            StartVertexY = vertices[21].Y,
                            EndVertexX = vertices[5].X,
                            EndVertexY = vertices[5].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[15],vertices[14], vertices[21], vertices[20] }
                },
                new Room()
                {
                    RoomId = 8,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[3].X,
                            StartVertexY = vertices[3].Y,
                            EndVertexX = vertices[2].X,
                            EndVertexY = vertices[2].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[2].X,
                            StartVertexY = vertices[2].Y,
                            EndVertexX = vertices[23].X,
                            EndVertexY = vertices[23].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[23].X,
                            StartVertexY = vertices[23].Y,
                            EndVertexX = vertices[22].X,
                            EndVertexY = vertices[22].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[22].X,
                            StartVertexY = vertices[22].Y,
                            EndVertexX = vertices[3].X,
                            EndVertexY = vertices[3].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[3],vertices[2],vertices[23],vertices[22]}
                },
                new Room()
                {
                    RoomId = 9,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[20].X,
                            StartVertexY = vertices[20].Y,
                            EndVertexX = vertices[18].X,
                            EndVertexY = vertices[18].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[18].X,
                            StartVertexY = vertices[18].Y,
                            EndVertexX = vertices[24].X,
                            EndVertexY = vertices[24].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[24].X,
                            StartVertexY = vertices[24].Y,
                            EndVertexX = vertices[25].X,
                            EndVertexY = vertices[25].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[25].X,
                            StartVertexY = vertices[25].Y,
                            EndVertexX = vertices[20].X,
                            EndVertexY = vertices[20].Y
                        },
                    }
                   // Vertexes = new List<Vertex>() {vertices[20],vertices[18],vertices[24],vertices[25]}
                },
                new Room()
                {
                    RoomId = 10,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[27].X,
                            StartVertexY = vertices[27].Y,
                            EndVertexX = vertices[26].X,
                            EndVertexY = vertices[26].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[26].X,
                            StartVertexY = vertices[26].Y,
                            EndVertexX = vertices[23].X,
                            EndVertexY = vertices[23].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[23].X,
                            StartVertexY = vertices[23].Y,
                            EndVertexX = vertices[22].X,
                            EndVertexY = vertices[22].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[22].X,
                            StartVertexY = vertices[22].Y,
                            EndVertexX = vertices[27].X,
                            EndVertexY = vertices[27].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[27],vertices[26],vertices[23],vertices[22]}
                },
                new Room()
                {
                    RoomId = 11,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[28].X,
                            StartVertexY = vertices[28].Y,
                            EndVertexX = vertices[29].X,
                            EndVertexY = vertices[29].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[29].X,
                            StartVertexY = vertices[29].Y,
                            EndVertexX = vertices[30].X,
                            EndVertexY = vertices[30].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[30].X,
                            StartVertexY = vertices[30].Y,
                            EndVertexX = vertices[31].X,
                            EndVertexY = vertices[31].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[31].X,
                            StartVertexY = vertices[31].Y,
                            EndVertexX = vertices[28].X,
                            EndVertexY = vertices[28].Y
                        },
                    }
                   // Vertexes = new List<Vertex>() {vertices[28],vertices[29],vertices[30],vertices[31]}
                },
                new Room()
                {
                    RoomId = 12,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[27].X,
                            StartVertexY = vertices[27].Y,
                            EndVertexX = vertices[26].X,
                            EndVertexY = vertices[26].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[26].X,
                            StartVertexY = vertices[26].Y,
                            EndVertexX = vertices[35].X,
                            EndVertexY = vertices[35].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[35].X,
                            StartVertexY = vertices[35].Y,
                            EndVertexX = vertices[34].X,
                            EndVertexY = vertices[34].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[34].X,
                            StartVertexY = vertices[34].Y,
                            EndVertexX = vertices[33].X,
                            EndVertexY = vertices[33].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[33].X,
                            StartVertexY = vertices[33].Y,
                            EndVertexX = vertices[32].X,
                            EndVertexY = vertices[32].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[32].X,
                            StartVertexY = vertices[32].Y,
                            EndVertexX = vertices[27].X,
                            EndVertexY = vertices[27].Y
                        }
                    }
                    //Vertexes = new List<Vertex>() {vertices[27],vertices[26],vertices[35],vertices[34], vertices[33], vertices[32] }
                },
                new Room()
                {
                    RoomId = 13,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[33].X,
                            StartVertexY = vertices[33].Y,
                            EndVertexX = vertices[34].X,
                            EndVertexY = vertices[34].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[34].X,
                            StartVertexY = vertices[34].Y,
                            EndVertexX = vertices[36].X,
                            EndVertexY = vertices[36].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[36].X,
                            StartVertexY = vertices[36].Y,
                            EndVertexX = vertices[37].X,
                            EndVertexY = vertices[37].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[37].X,
                            StartVertexY = vertices[37].Y,
                            EndVertexX = vertices[33].X,
                            EndVertexY = vertices[33].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[33],vertices[34],vertices[37],vertices[36]}
                },
                new Room()
                {
                    RoomId = 14,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[37].X,
                            StartVertexY = vertices[37].Y,
                            EndVertexX = vertices[36].X,
                            EndVertexY = vertices[36].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[36].X,
                            StartVertexY = vertices[36].Y,
                            EndVertexX = vertices[38].X,
                            EndVertexY = vertices[38].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[38].X,
                            StartVertexY = vertices[38].Y,
                            EndVertexX = vertices[39].X,
                            EndVertexY = vertices[39].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[39].X,
                            StartVertexY = vertices[39].Y,
                            EndVertexX = vertices[37].X,
                            EndVertexY = vertices[37].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[37],vertices[36],vertices[38],vertices[39]}
                },
                new Room()
                {
                    RoomId = 15,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[39].X,
                            StartVertexY = vertices[39].Y,
                            EndVertexX = vertices[38].X,
                            EndVertexY = vertices[38].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[38].X,
                            StartVertexY = vertices[38].Y,
                            EndVertexX = vertices[40].X,
                            EndVertexY = vertices[40].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[40].X,
                            StartVertexY = vertices[40].Y,
                            EndVertexX = vertices[41].X,
                            EndVertexY = vertices[41].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[41].X,
                            StartVertexY = vertices[41].Y,
                            EndVertexX = vertices[39].X,
                            EndVertexY = vertices[39].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[39],vertices[38],vertices[41],vertices[40]}
                },
                new Room()
                {
                    RoomId = 16,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[41].X,
                            StartVertexY = vertices[41].Y,
                            EndVertexX = vertices[40].X,
                            EndVertexY = vertices[40].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[40].X,
                            StartVertexY = vertices[40].Y,
                            EndVertexX = vertices[42].X,
                            EndVertexY = vertices[42].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[42].X,
                            StartVertexY = vertices[42].Y,
                            EndVertexX = vertices[43].X,
                            EndVertexY = vertices[43].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[43].X,
                            StartVertexY = vertices[43].Y,
                            EndVertexX = vertices[41].X,
                            EndVertexY = vertices[41].Y
                        },
                    }
                   // Vertexes = new List<Vertex>() {vertices[41],vertices[40],vertices[42],vertices[43]}
                },
                new Room()
                {
                    RoomId = 17,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[43].X,
                            StartVertexY = vertices[43].Y,
                            EndVertexX = vertices[42].X,
                            EndVertexY = vertices[42].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[42].X,
                            StartVertexY = vertices[42].Y,
                            EndVertexX = vertices[47].X,
                            EndVertexY = vertices[47].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[47].X,
                            StartVertexY = vertices[47].Y,
                            EndVertexX = vertices[31].X,
                            EndVertexY = vertices[31].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[31].X,
                            StartVertexY = vertices[31].Y,
                            EndVertexX = vertices[30].X,
                            EndVertexY = vertices[30].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[30].X,
                            StartVertexY = vertices[30].Y,
                            EndVertexX = vertices[44].X,
                            EndVertexY = vertices[44].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[44].X,
                            StartVertexY = vertices[44].Y,
                            EndVertexX = vertices[43].X,
                            EndVertexY = vertices[43].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[43],vertices[42],vertices[44],vertices[30], vertices[31], vertices[47] }
                },
                new Room()
                {
                    RoomId = 18,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[50].X,
                            StartVertexY = vertices[50].Y,
                            EndVertexX = vertices[49].X,
                            EndVertexY = vertices[49].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[49].X,
                            StartVertexY = vertices[49].Y,
                            EndVertexX = vertices[52].X,
                            EndVertexY = vertices[52].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[52].X,
                            StartVertexY = vertices[52].Y,
                            EndVertexX = vertices[53].X,
                            EndVertexY = vertices[53].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[53].X,
                            StartVertexY = vertices[53].Y,
                            EndVertexX = vertices[50].X,
                            EndVertexY = vertices[50].Y
                        },
                    }
                   // Vertexes = new List<Vertex>() {vertices[50],vertices[49],vertices[53],vertices[52]}
                },
                new Room()
                {
                    RoomId = 19,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[61].X,
                            StartVertexY = vertices[61].Y,
                            EndVertexX = vertices[54].X,
                            EndVertexY = vertices[54].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[54].X,
                            StartVertexY = vertices[54].Y,
                            EndVertexX = vertices[55].X,
                            EndVertexY = vertices[55].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[55].X,
                            StartVertexY = vertices[55].Y,
                            EndVertexX = vertices[56].X,
                            EndVertexY = vertices[56].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[56].X,
                            StartVertexY = vertices[56].Y,
                            EndVertexX = vertices[61].X,
                            EndVertexY = vertices[61].Y
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[61],vertices[54],vertices[55],vertices[56]}
                },
                new Room()
                {
                    RoomId = 20,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[54].X,
                            StartVertexY = vertices[54].Y,
                            EndVertexX = vertices[55].X,
                            EndVertexY = vertices[55].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[55].X,
                            StartVertexY = vertices[55].Y,
                            EndVertexX = vertices[58].X,
                            EndVertexY = vertices[58].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[58].X,
                            StartVertexY = vertices[58].Y,
                            EndVertexX = vertices[57].X,
                            EndVertexY = vertices[57].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[57].X,
                            StartVertexY = vertices[57].Y,
                            EndVertexX = vertices[54].X,
                            EndVertexY = vertices[54].Y
                        },
                    }
                   // Vertexes = new List<Vertex>() {vertices[54],vertices[55],vertices[58],vertices[57]}
                },
                new Room()
                {
                    RoomId = 21,
                    
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertexX = vertices[57].X,
                            StartVertexY = vertices[57].Y,
                            EndVertexX = vertices[58].X,
                            EndVertexY = vertices[58].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[58].X,
                            StartVertexY = vertices[58].Y,
                            EndVertexX = vertices[59].X,
                            EndVertexY = vertices[59].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[59].X,
                            StartVertexY = vertices[59].Y,
                            EndVertexX = vertices[60].X,
                            EndVertexY = vertices[60].Y
                        },
                        new Wall()
                        {
                            StartVertexX = vertices[60].X,
                            StartVertexY = vertices[60].Y,
                            EndVertexX = vertices[57].X,
                            EndVertexY = vertices[57].Y
                        },
                    }
                   // Vertexes = new List<Vertex>() {vertices[57],vertices[58],vertices[60],vertices[59]}
                }
            };

            IList<Project> projects  = new List<Project>()
            {
                new Project()
                {
                    Description = "ProjectsMap - projekt zespolowy",
                    DocumentationLink = "documentationlink",
                    RepositoryLink = "repositoryLink"
                }
            };

            IList<Technology> technologies = new List<Technology>()
            {
                new Technology()
                {
                    Name = "Angular"
                },
                new Technology()
                {
                    Name = "HTML"
                },
                new Technology()
                {
                    Name = "C#"
                },
                new Technology()
                {
                    Name = "Java"
                },
                new Technology()
                {
                    Name = "C++"
                },
                new Technology()
                {
                    Name = "CSS"
                }
            };


            IList<Employee> developers = new List<Employee>()
            {
                new Employee()
                {
                    FirstName = "Witkor",
                    Surname = "Bukowski",
                    Email = "wiktor@gmail.com",
                    JobTitle = "Spawacz",
                    EmployeeId = 1,
                    Technologies = new List<Technology>()
                    {
                       technologies[0],technologies[3]
                    },
                   ProjectRoles = new List<ProjectRole>()
                   {
                       new ProjectRole()
                       {
                           Role = "Manager",
                           Project = projects[0]
                       }
                   }
                },
                new Employee()
                {
                    FirstName = "Michal",
                    Surname = "Radziwilko",
                    EmployeeId = 2,
                    Technologies = new List<Technology>()
                    {
                        technologies[5],technologies[1]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Jan",
                    Surname = "Kowalski",
                    EmployeeId = 3,
                    Technologies = new List<Technology>()
                    {
                        technologies[5],technologies[1]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Tadeusz",
                    Surname = "Nowak",
                    EmployeeId = 4,
                    Technologies = new List<Technology>()
                    {
                        technologies[0],technologies[4]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Kacper",
                    Surname = "Nowak",
                   EmployeeId = 5,
                    Technologies = new List<Technology>()
                    {
                        technologies[2]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Joanna",
                    Surname = "Wojciechowska",
                    EmployeeId = 6,
                    Technologies = new List<Technology>()
                    {
                        technologies[5],technologies[1]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Katarzyna",
                    Surname = "Zajac",
                    EmployeeId = 7,
                    Technologies = new List<Technology>()
                    {
                        technologies[5],technologies[1],technologies[0]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Dawid",
                    Surname = "Olszewski",
                   EmployeeId = 8,
                    Technologies = new List<Technology>()
                    {
                        technologies[2],technologies[0]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Michał",
                    Surname = "Wieczorek",
                    EmployeeId = 9,
                    Technologies = new List<Technology>()
                    {
                        technologies[5],technologies[1], technologies[4]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Daniel",
                    Surname = "Malinowski",
                    EmployeeId = 10,
                    Technologies = new List<Technology>()
                    {
                        technologies[5],technologies[1]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Dawid",
                    Surname = "Adamczyk",
                   EmployeeId = 11,
                    Technologies = new List<Technology>()
                    {
                        technologies[2],technologies[0]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Grzegorz",
                    Surname = "Piotrowski",
                   EmployeeId = 12,
                    Technologies = new List<Technology>()
                    {
                        technologies[5],technologies[1],technologies[4]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Adrian",
                    Surname = "Kowalski",
                   EmployeeId = 13,
                    Technologies = new List<Technology>()
                    {
                        technologies[5],technologies[1], technologies[0],technologies[3]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Anna",
                    Surname = "Rutokowska",
                    EmployeeId = 14,
                    Technologies = new List<Technology>()
                    {
                        technologies[2],technologies[0]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Tomasz",
                    Surname = "Grabowski",
                    EmployeeId = 15,
                    Technologies = new List<Technology>()
                    {
                        technologies[2],technologies[0]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Natalia",
                    Surname = "Kozłowska",
                   EmployeeId = 16,
                    Technologies = new List<Technology>()
                    {
                        technologies[2],technologies[1],technologies[0]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Szymon",
                    Surname = "Zalewski",
                   EmployeeId = 17,
                    Technologies = new List<Technology>()
                    {
                        technologies[2],technologies[1]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Ewa",
                    Surname = "Witkowska",
                    EmployeeId = 18,
                    Technologies = new List<Technology>()
                    {
                        technologies[5],technologies[1],technologies[0]
                    },
                    ProjectRoles = new List<ProjectRole>()
                    {
                        new ProjectRole()
                        {
                            Role = "Employee",
                            Project = projects[0]
                        }
                    }
                },
            };

            foreach (var dev in developers)
            {
                dev.ProjectRoles.ToList()[0].EmployeeId = dev.EmployeeId;
                dev.ProjectRoles.ToList()[0].Employee = dev;
                if (dev.EmployeeId != 1)
                {
                    dev.ManagerId = 1;
                    dev.Manager = developers[0];
                }
            }

            IList<User> users = new List<User>
            {
                new User()
                {
            //        UserId = 1,
                    Created = DateTime.Now,
                    Employee = developers[0],
                    Username = "Wiktor",
                    Password = "secret",
                    Email = "michal@gmail.com"
                },
                new User()
                {
            //        UserId = 2,
                    Created = DateTime.Now,
                    Employee = developers[1],
                    Username = "Michal",
                    Password = "secret2",
                    Email = "wiktor@gmail.com"
                },
                new User()
                {
          //          UserId = 3,
                    Created = DateTime.Now,
                    Employee = developers[2],
                    Username = "Parcownik1",
                    Password = "secret",
                    Email = "pracownik1@gmail.com"
                },
                new User()
                {
          //          UserId = 4,
                    Created = DateTime.Now,
                    Employee = developers[3],
                    Username = "Parcownik2",
                    Password = "secret",
                    Email = "pracownik2@gmail.com"
                },
                new User()
                {
          //          UserId = 5,
                    Created = DateTime.Now,
                    Employee = developers[4],
                    Username = "Parcownik3",
                    Password = "secret",
                    Email = "pracownik3@gmail.com"
                },
                new User()
                {
          //          UserId = 6,
                    Created = DateTime.Now,
                    Employee = developers[5],
                    Username = "Parcownik4",
                    Password = "secret",
                    Email = "pracownik4@gmail.com"
                },
                new User()
                {
          //          UserId = 7,
                    Created = DateTime.Now,
                    Employee = developers[6],
                    Username = "Parcownik5",
                    Password = "secret",
                    Email = "pracownik5@gmail.com"
                },
                new User()
                {
          //          UserId = 8,
                    Created = DateTime.Now,
                    Employee = developers[7],
                    Username = "Parcownik6",
                    Password = "secret",
                    Email = "pracownik6@gmail.com"
                },
                    new User()
                {
          //          UserId = 9,
                    Created = DateTime.Now,
                    Employee = developers[8],
                    Username = "Parcownik7",
                    Password = "secret",
                    Email = "pracownik7@gmail.com"
                },
                    new User()
                {
          //          UserId = 10,
                    Created = DateTime.Now,
                    Employee = developers[9],
                    Username = "Parcownik8",
                    Password = "secret",
                    Email = "pracownik8@gmail.com"
                },
                    new User()
                {
          //          UserId = 11,
                    Created = DateTime.Now,
                    Employee = developers[10],
                    Username = "Parcownik9",
                    Password = "secret",
                    Email = "pracownik9@gmail.com"
                },
                    new User()
                {
          //          UserId = 12,
                    Created = DateTime.Now,
                    Employee = developers[11],
                    Username = "Parcownik10",
                    Password = "secret",
                    Email = "pracownik10@gmail.com"
                },
                    new User()
                {
          //          UserId = 13,
                    Created = DateTime.Now,
                    Employee = developers[12],
                    Username = "Parcownik11",
                    Password = "secret",
                    Email = "pracownik11@gmail.com"
                },
                    new User()
                {
          //          UserId = 14,
                    Created = DateTime.Now,
                    Employee = developers[13],
                    Username = "Parcownik12",
                    Password = "secret",
                    Email = "pracownik12@gmail.com"
                },
                    new User()
                {
          //          UserId = 15,
                    Created = DateTime.Now,
                    Employee = developers[14],
                    Username = "Parcownik13",
                    Password = "secret",
                    Email = "pracownik13@gmail.com"
                },
                    new User()
                {
          //          UserId = 16,
                    Created = DateTime.Now,
                    Employee = developers[15],
                    Username = "Parcownik14",
                    Password = "secret",
                    Email = "pracownik14@gmail.com"
                },
                    new User()
                {
          //          UserId = 17,
                    Created = DateTime.Now,
                    Employee = developers[16],
                    Username = "Parcownik15",
                    Password = "secret",
                    Email = "pracownik15@gmail.com"
                },
                    new User()
                {
          //          UserId = 18,
                    Created = DateTime.Now,
                    Employee = developers[17],
                    Username = "Parcownik16",
                    Password = "secret",
                    Email = "pracownik16@gmail.com"
                },
            };

            IList<Seat> seatsFirstRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 1,
                    Employee = developers[0],
                    X = vertices[4].X,
                    Y = vertices[4].Y,

                },
                new Seat()
                {
                    SeatId = 2,
                    Employee = developers[1],
                    X = vertices[5].X,
                    Y = vertices[5].Y,
                },
                new Seat()
                {
                    SeatId = 3,
                    Employee = developers[2],
                    X = vertices[1].X,
                    Y = vertices[1].Y,
                },
                new Seat()
                {
                    SeatId = 4,
                    Employee = developers[3],
                    X = vertices[71].X,
                    Y = vertices[71].Y,
                },
            };            
            IList<Seat> seatsThirdRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 5,
                    Employee = developers[10],
                    X = vertices[16].X,
                    Y = vertices[16].Y,
                },
                new Seat()
                {
                    SeatId = 6,
                    Employee = developers[11],
                    X = vertices[17].X,
                    Y = vertices[17].Y,
                },
            };
            IList<Seat> seatsFourthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 7,
                    Employee = null,
                    X = vertices[19].X,
                    Y = vertices[19].Y,
                },
                new Seat()
                {
                    SeatId = 8,
                    Employee = null,
                    X = vertices[62].X,
                    Y = vertices[62].Y,
                },
            };
            IList<Seat> seatsFifthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 9,
                    Employee = null,
                    X = vertices[63].X,
                    Y = vertices[63].Y,
                },
                new Seat()
                {
                    SeatId = 10,
                    Employee = null,
                    X = vertices[64].X,
                    Y = vertices[64].Y,
                },
            };
            IList<Seat> seatsSixthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 11,
                    Employee = null,
                    X = vertices[65].X,
                    Y = vertices[65].Y,
                },
                new Seat()
                {
                    SeatId = 12,
                    Employee = null,
                    X = vertices[66].X,
                    Y = vertices[66].Y,
                },
            };


             // projects[0].Employees = developers;
            projects[0].Technologies = new List<Technology>()
            {
                developers.ToList()[0].Technologies.ToList()[0],
                developers.ToList()[0].Technologies.ToList()[1],
                developers.ToList()[1].Technologies.ToList()[0],
                developers.ToList()[1].Technologies.ToList()[1]
            };

            IList<Seat> seatsSeventhRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 13,
                    Employee = null,
                    X = vertices[67].X,
                    Y = vertices[67].Y,

                },
                new Seat()
                { 
                    SeatId = 14,
                    Employee = null,
                    X = vertices[68].X,
                    Y = vertices[68].Y,
                },
                new Seat()
                {
                    SeatId = 15,
                    Employee = null,
                    X = vertices[69].X,
                    Y = vertices[69].Y,
                },
                new Seat()
                {
                    SeatId = 16,
                    Employee = null,
                    X = vertices[70].X,
                    Y = vertices[70].Y,
                },
            };
            IList<Seat> seatsSecondRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 17,
                    Employee = developers[4],
                    X = vertices[72].X,
                    Y = vertices[72].Y,
                },
                new Seat()
                {
                    SeatId = 18,
                    Employee = developers[5],
                    X = vertices[73].X,
                    Y = vertices[73].Y,
                },
                new Seat()
                {
                    SeatId = 19,
                    Employee = developers[6],
                    X = vertices[74].X,
                    Y = vertices[74].Y,
                },
                new Seat()
                {
                    SeatId = 20,
                    Employee = developers[7],
                    X = vertices[75].X,
                    Y = vertices[75].Y,
                },
                new Seat()
                {
                    SeatId = 21,
                    Employee = developers[8],
                    X = vertices[76].X,
                    Y = vertices[76].Y,
                },
                new Seat()
                {
                    SeatId = 22,
                    Employee = developers[9],
                    X = vertices[77].X,
                    Y = vertices[77].Y,
                },
                new Seat()
                {
                    SeatId = 23,
                    Employee = null,
                    X = vertices[78].X,
                    Y = vertices[78].Y,
                },
                new Seat()
                {
                    SeatId = 24,
                    Employee = null,
                    X = vertices[79].X,
                    Y = vertices[79].Y,
                },
            };
            IList<Seat> seatsEighteenthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 25,
                    Employee = null,
                    X = vertices[80].X,
                    Y = vertices[80].Y,
                },
                new Seat()
                {
                    SeatId = 26,
                    Employee = null,
                    X = vertices[81].X,
                    Y = vertices[81].Y,
                },
                new Seat()
                {
                    SeatId = 27,
                    Employee = null,
                    X = vertices[82].X,
                    Y = vertices[82].Y,
                },
                new Seat()
                {
                    SeatId = 28,
                    Employee = null,
                    X = vertices[83].X,
                    Y = vertices[83].Y,
                },
                new Seat()
                {
                    SeatId = 29,
                    Employee = null,
                    X = vertices[84].X,
                    Y = vertices[84].Y,
                },
                new Seat()
                {
                    SeatId = 30,
                    Employee = null,
                    X = vertices[85].X,
                    Y = vertices[85].Y,
                },
                new Seat()
                {
                    SeatId = 31,
                    Employee = null,
                    X = vertices[86].X,
                    Y = vertices[86].Y,
                },
                new Seat()
                {
                    SeatId = 32,
                    Employee = null,
                    X = vertices[87].X,
                    Y = vertices[87].Y,
                },
            };
            IList<Seat> seatsEighthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 33,
                    Employee = developers[12],
                    X = vertices[88].X,
                    Y = vertices[88].Y,
                },
                new Seat()
                {
                    SeatId = 34,
                    Employee = developers[13],
                    X = vertices[89].X,
                    Y = vertices[89].Y,
                },
            };
            IList<Seat> seatsTenthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 35,
                    Employee = developers[14],
                    X = vertices[90].X,
                    Y = vertices[90].Y,
                },
                new Seat()
                {
                    SeatId = 36,
                    Employee = developers[15],
                    X = vertices[91].X,
                    Y = vertices[91].Y,
                },
            };
            IList<Seat> seatsThirteenthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 37,
                    Employee = developers[16],
                    X = vertices[92].X,
                    Y = vertices[92].Y,
                },
            };
            IList<Seat> seatsFourteenthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 38,
                    Employee = developers[17],
                    X = vertices[93].X,
                    Y = vertices[93].Y,
                },
            };
            IList<Seat> seatsFifteenthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 39,
                    Employee = null,
                    X = vertices[94].X,
                    Y = vertices[94].Y,
                },
            };
            IList<Seat> seatsSixteenthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 40,
                    Employee = null,
                    X = vertices[95].X,
                    Y = vertices[95].Y,
                },
            };
            IList<Seat> seatsTwelfthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 41,
                    Employee = null,
                    X = vertices[96].X,
                    Y = vertices[96].Y,
                },
                new Seat()
                {
                    SeatId = 42,
                    Employee = null,
                    X = vertices[97].X,
                    Y = vertices[97].Y,
                },
                new Seat()
                {
                    SeatId = 43,
                    Employee = null,
                    X = vertices[98].X,
                    Y = vertices[98].Y,
                },
                new Seat()
                {
                    SeatId = 44,
                    Employee = null,
                    X = vertices[99].X,
                    Y = vertices[99].Y,
                },
                new Seat()
                {
                    SeatId = 45,
                    Employee = null,
                    X = vertices[100].X,
                    Y = vertices[100].Y,
                },
                new Seat()
                {
                    SeatId = 46,
                    Employee = null,
                    X = vertices[101].X,
                    Y = vertices[101].Y,
                },
                new Seat()
                {
                    SeatId = 47,
                    Employee = null,
                    X = vertices[102].X,
                    Y = vertices[102].Y,
                },
                new Seat()
                {
                    SeatId = 48,
                    Employee = null,
                    X = vertices[103].X,
                    Y = vertices[103].Y,
                },
            };
            IList<Seat> seatsSeventeenthRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 49,
                    Employee = null,
                    X = vertices[104].X,
                    Y = vertices[104].Y,
                },
                new Seat()
                {
                    SeatId = 50,
                    Employee = null,
                    X = vertices[105].X,
                    Y = vertices[105].Y,
                },
                new Seat()
                {
                    SeatId = 51,
                    Employee = null,
                    X = vertices[106].X,
                    Y = vertices[106].Y,
                },
                new Seat()
                {
                    SeatId = 52,
                    Employee = null,
                    X = vertices[107].X,
                    Y = vertices[107].Y,
                },
                new Seat()
                {
                    SeatId = 53,
                    Employee = null,
                    X = vertices[108].X,
                    Y = vertices[108].Y,
                },
                new Seat()
                {
                    SeatId = 54,
                    Employee = null,
                    X = vertices[109].X,
                    Y = vertices[109].Y,
                },
                new Seat()
                {
                    SeatId = 55,
                    Employee = null,
                    X = vertices[110].X,
                    Y = vertices[110].Y,
                },
                new Seat()
                {
                    SeatId = 56,
                    Employee = null,
                    X = vertices[111].X,
                    Y = vertices[111].Y,
                },
            };
            
            rooms[0].Seats = seatsFirstRoom;
            seatsFirstRoom[0].RoomId = 1;
            seatsFirstRoom[0].Room = rooms[0];
            seatsFirstRoom[1].RoomId = 1;
            seatsFirstRoom[1].Room = rooms[0];

            rooms[2].Seats = seatsThirdRoom;
            seatsThirdRoom[0].RoomId = 3;
            seatsThirdRoom[0].Room = rooms[2];
            seatsThirdRoom[1].RoomId = 3;
            seatsThirdRoom[1].Room = rooms[2];

            rooms[3].Seats = seatsFourthRoom;
            seatsFourthRoom[0].RoomId = 4;
            seatsFourthRoom[0].Room = rooms[3];
            seatsFourthRoom[1].RoomId = 4;
            seatsFourthRoom[1].Room = rooms[3];

            rooms[4].Seats = seatsFifthRoom;
            seatsFifthRoom[0].RoomId = 5;
            seatsFifthRoom[0].Room = rooms[4];
            seatsFifthRoom[1].RoomId = 5;
            seatsFifthRoom[1].Room = rooms[4];

            rooms[5].Seats = seatsSixthRoom;
            seatsSixthRoom[0].RoomId = 6;
            seatsSixthRoom[0].Room = rooms[5];
            seatsSixthRoom[1].RoomId = 6;
            seatsSixthRoom[1].Room = rooms[5];

            rooms[6].Seats = seatsSeventhRoom;
            seatsSeventhRoom[0].RoomId = 7;
            seatsSeventhRoom[0].Room = rooms[6];
            seatsSeventhRoom[1].RoomId = 7;
            seatsSeventhRoom[1].Room = rooms[6];
            seatsSeventhRoom[2].RoomId = 7;
            seatsSeventhRoom[2].Room = rooms[6];
            seatsSeventhRoom[3].RoomId = 7;
            seatsSeventhRoom[3].Room = rooms[6];

            rooms[1].Seats = seatsSecondRoom;
            seatsSecondRoom[0].RoomId = 2;
            seatsSecondRoom[0].Room = rooms[1];
            seatsSecondRoom[1].RoomId = 2;
            seatsSecondRoom[1].Room = rooms[1];
            seatsSecondRoom[2].RoomId = 2;
            seatsSecondRoom[2].Room = rooms[1];
            seatsSecondRoom[3].RoomId = 2;
            seatsSecondRoom[3].Room = rooms[1];
            seatsSecondRoom[4].RoomId = 2;
            seatsSecondRoom[4].Room = rooms[1];
            seatsSecondRoom[5].RoomId = 2;
            seatsSecondRoom[5].Room = rooms[1];
            seatsSecondRoom[6].RoomId = 2;
            seatsSecondRoom[6].Room = rooms[1];
            seatsSecondRoom[7].RoomId = 2;
            seatsSecondRoom[7].Room = rooms[1];

            rooms[17].Seats = seatsEighteenthRoom;
            seatsEighteenthRoom[0].RoomId = 18;
            seatsEighteenthRoom[0].Room = rooms[17];
            seatsEighteenthRoom[1].RoomId = 18;
            seatsEighteenthRoom[1].Room = rooms[17];
            seatsEighteenthRoom[2].RoomId = 18;
            seatsEighteenthRoom[2].Room = rooms[17];
            seatsEighteenthRoom[3].RoomId = 18;
            seatsEighteenthRoom[3].Room = rooms[17];
            seatsEighteenthRoom[4].RoomId = 18;
            seatsEighteenthRoom[4].Room = rooms[17];
            seatsEighteenthRoom[5].RoomId = 18;
            seatsEighteenthRoom[5].Room = rooms[17];
            seatsEighteenthRoom[6].RoomId = 18;
            seatsEighteenthRoom[6].Room = rooms[17];
            seatsEighteenthRoom[7].RoomId = 18;
            seatsEighteenthRoom[7].Room = rooms[17];

            rooms[7].Seats = seatsEighthRoom;
            seatsEighthRoom[0].RoomId = 8;
            seatsEighthRoom[0].Room = rooms[7];
            seatsEighthRoom[1].RoomId = 8;
            seatsEighthRoom[1].Room = rooms[7];

            rooms[9].Seats = seatsTenthRoom;
            seatsTenthRoom[0].RoomId = 10;
            seatsTenthRoom[0].Room = rooms[9];
            seatsTenthRoom[1].RoomId = 10;
            seatsTenthRoom[1].Room = rooms[9];

            rooms[12].Seats = seatsThirteenthRoom;
            seatsThirteenthRoom[0].RoomId = 13;
            seatsThirteenthRoom[0].Room = rooms[12];

            rooms[13].Seats = seatsFourteenthRoom;
            seatsFourteenthRoom[0].RoomId = 14;
            seatsFourteenthRoom[0].Room = rooms[13];

            rooms[14].Seats = seatsFifteenthRoom;
            seatsFifteenthRoom[0].RoomId = 15;
            seatsFifteenthRoom[0].Room = rooms[14];

            rooms[15].Seats = seatsSixteenthRoom;
            seatsSixteenthRoom[0].RoomId = 16;
            seatsSixteenthRoom[0].Room = rooms[15];

            rooms[11].Seats = seatsTwelfthRoom;
            seatsTwelfthRoom[0].RoomId = 12;
            seatsTwelfthRoom[0].Room = rooms[11];
            seatsTwelfthRoom[1].RoomId = 12;
            seatsTwelfthRoom[1].Room = rooms[11];
            seatsTwelfthRoom[2].RoomId = 12;
            seatsTwelfthRoom[2].Room = rooms[11];
            seatsTwelfthRoom[3].RoomId = 12;
            seatsTwelfthRoom[3].Room = rooms[11];
            seatsTwelfthRoom[4].RoomId = 12;
            seatsTwelfthRoom[4].Room = rooms[11];
            seatsTwelfthRoom[5].RoomId = 12;
            seatsTwelfthRoom[5].Room = rooms[11];
            seatsTwelfthRoom[6].RoomId = 12;
            seatsTwelfthRoom[6].Room = rooms[11];
            seatsTwelfthRoom[7].RoomId = 12;
            seatsTwelfthRoom[7].Room = rooms[11];

            rooms[16].Seats = seatsSeventeenthRoom;
            seatsSeventeenthRoom[0].RoomId = 17;
            seatsSeventeenthRoom[0].Room = rooms[16];
            seatsSeventeenthRoom[1].RoomId = 17;
            seatsSeventeenthRoom[1].Room = rooms[16];
            seatsSeventeenthRoom[2].RoomId = 17;
            seatsSeventeenthRoom[2].Room = rooms[16];
            seatsSeventeenthRoom[3].RoomId = 17;
            seatsSeventeenthRoom[3].Room = rooms[16];
            seatsSeventeenthRoom[4].RoomId = 17;
            seatsSeventeenthRoom[4].Room = rooms[16];
            seatsSeventeenthRoom[5].RoomId = 17;
            seatsSeventeenthRoom[5].Room = rooms[16];
            seatsSeventeenthRoom[6].RoomId = 17;
            seatsSeventeenthRoom[6].Room = rooms[16];
            seatsSeventeenthRoom[7].RoomId = 17;
            seatsSeventeenthRoom[7].Room = rooms[16];

            developers[0].User = users[0];
            developers[0].Seat = seatsFirstRoom[0];
            developers[1].User = users[1];
            developers[1].Seat = seatsFirstRoom[1];
            developers[1].User = users[1];
            developers[2].Seat = seatsFirstRoom[2];
            developers[2].User = users[2];
            developers[3].Seat = seatsFirstRoom[3];
            developers[3].User = users[3];
            developers[4].Seat = seatsSecondRoom[0];
            developers[4].User = users[4];
            developers[5].Seat = seatsSecondRoom[1];
            developers[5].User = users[5];
            developers[6].Seat = seatsSecondRoom[2];
            developers[6].User = users[6];
            developers[7].Seat = seatsSecondRoom[3];
            developers[7].User = users[7];
            developers[8].Seat = seatsSecondRoom[4];
            developers[8].User = users[8];
            developers[9].Seat = seatsSecondRoom[5];
            developers[9].User = users[9];
            developers[10].Seat = seatsThirdRoom[0];
            developers[10].User = users[10];
            developers[11].Seat = seatsThirdRoom[1];
            developers[11].User = users[11];
            developers[12].Seat = seatsEighthRoom[0];
            developers[12].User = users[12];
            developers[13].Seat = seatsEighthRoom[1];
            developers[13].User = users[13];
            developers[14].Seat = seatsTenthRoom[0];
            developers[14].User = users[14];
            developers[15].Seat = seatsTenthRoom[1];
            developers[15].User = users[15];
            developers[16].Seat = seatsThirteenthRoom[0];
            developers[16].User = users[16];
            developers[17].Seat = seatsFourteenthRoom[0];
            developers[17].User = users[17];

            var company = new Company()
            {
                CompanyId = 1,
                Buildings = new List<Building>()
                {
                    new Building()
                    {
                        BuildingId = 1,
                        CompanyId = 1,
                        Address = "Wroclaw, Poznanska 54A",
                        Floors = new List<Floor>()
                        {
                            new Floor()
                            {
                                BuildingId = 1,
                                Description = "Pietro 3",
                                FloorId = 1,
                                Rooms = rooms
                            }
                        }
                    }
                },
                Employees = developers,
                Projects = projects
                
            };
            projects.ToList()[0].Company = company;
            company.Buildings.ToList()[0].Floors.ToList()[0].Building = company.Buildings.ToList()[0];

            company.Buildings.ToList()[0].Company = company;

            context.Companies.Add(company);

            context.SaveChanges();        

        }
    }
}