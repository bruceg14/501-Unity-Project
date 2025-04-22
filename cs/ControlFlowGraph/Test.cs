class Test {
    void ifTest() {
        int x = 1;
        if (x == 1) {
            if (x > 0) {
                x = 1;
            }
            x = 2;
        } else {
            x = 3;
        }
    }

    void tmpTest() {
        for (int i = 0 ; i < 10; ++i) {
            if (i == 5) {
                break;
            } else {
                Console.WriteLine(i);
            }
        }
    }
    void whileTest() {
        int a  = 1;
        while(a < 10) {
            if(a == 5) {
                a --;
            }

            a += 3;
        }

        int b = 1;
    }

    void forTest() {
        for (int i = 0; i < 10; ++i) {
            i += 1;
        }
        int a = 2;
    }

    void ifTest1() {
        int x = 1;
        if (x == 1) {
            x += 1;
        } else {
            x = 3;
        }
    }

    void switchTest() {
        int x = 1;
        switch (x) {
            case 1:
                if (x == 1) {
                    x += 1;
                } else {
                    x = 3;
                }
                break;
            case 2:
                for (int i = 0; i < 10; ++i) {
                    i += 1;
                }
                break;
            default:
                x = 4;
                break;
        }
    }

    void tmpTest(){
        var cols = new int[] {1, 2 , 3};
        foreach (int c in cols)
        {
           // Debug.Log(c.name);
            if (c == 1)
            {
                //Destroy(c.transform.parent.gameObject);
                int x = 0;
            }
            int a = 0;
        }
    }

}