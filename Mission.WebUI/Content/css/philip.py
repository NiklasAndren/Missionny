


#kopiera all nedstående kod och klistra in i Python2.6  IDLE (python GUI) för att sedan kunna köra programmet

# -*- coding: cp1252 -*-

import random

class Atom():#En klass för grundämnena 

    def __init__(self, namn, vikt, anr): #initiering av attributen
        self.namn = namn
        self.vikt = vikt
        self.anr = anr

    def __str__(self):
        return self.namn + self.vikt + self.anr
    
    def satt_namn(self, namn):
        self.namn = namn

    def satt_vikt(self, vikt):
        self.vikt = vikt

    def satt_anr(self, anr):
        self.anr = anr

    def hamta_namn(self):
        return self.namn
    
    def hamta_vikt(self):
        return self.vikt

    def hamta_anr(self):
        return self.anr

    def __cmp__(self, other): #En egen cmp för att kunna sortera!

    
        if self.vikt > other.vikt:
            return 1
        elif self.vikt < other.vikt:
            return -1
        elif self.vikt == other.vikt:
            return 0

            
class Periodiska_Systemet():
    lista = [] #En lista

    def lasa_in(self): #Funktion för att läsa in filen m.m.
       
     
        filen = open('avikt.txt', 'r') #läser in från filen atomer.txt
        namn = filen.readline().strip() #KOLLA UPP VARFÖR VARIABEL UTANFÖR OCH INNANFÖR WHILE-SLINGAN
        while namn: #Slinga som säger "medans det finns ett namn gör detta"
            
            vikt = filen.readline().strip()#läser in attribut till objektet atom     
            vikt = float(vikt)
            anr = 0
            anr = float(anr)
            
            self.lista.append(Atom(namn, vikt, anr))#Skapar objektet Atom, med attributen namn, vikt och anr... sen appendar den objekten i listan "lista"
            namn = filen.readline().strip()
            
        filen.close()

    def sortering(self):
        
        self.lista.sort()
      
    def atom_numrering(self):
        m = 1
        for n in self.lista:
            n.anr = m
            m = m+1
            
        
    def skriv_ut(self):#skriver ut informationen om atomerna
       

        for i in self.lista:
            
            print '---| Atom, nr', i.anr ,'|---'           
            print 'Namn:',i.namn #i.namn kallar på objektet i's attribut "namn"
            print 'Vikt',i.vikt, '\n'

    def undantag(self):
        
            
        a = self.lista.pop(17)
        self.lista.insert(18, a)

        a = self.lista.pop(26)
        self.lista.insert(27, a)

        a = self.lista.pop(51)
        self.lista.insert(52, a)

        a = self.lista.pop(89)
        self.lista.insert(90, a)

        a = self.lista.pop(91)
        self.lista.insert(92, a)
            

def trana_anr(): #Felhantering för str svar!
    P = Periodiska_Systemet()
    print '\nHEJ! Nu ska vi träna på Atomnummer!'
    i = random.randint(0, len(P.lista))
    korrekt = P.lista[i].anr
    antal = 1
    print 'Vilket atomnummer har', P.lista[i].namn , '?'
    gissa = raw_input('Vilket atomnummer söker vi?')
    if gissa == korrekt:
        antal > 4

    while antal <= 2 :
        print 'feeeel!'
        gissa = raw_input('Vilket atomnumer söker vi?')
        if gissa== korrekt:
            break
        antal = antal + 1

    print 'rätt svar var:', korrekt

    
def trana_namn(): #felhantering för int svar!
    P = Periodiska_Systemet()
    print '\n\nHej! Här i atomskolan kan vi lära dig allt!!! Nu ska du få lära dig atombeteckning, let''s go!'
    i = random.randint(0, len(P.lista))
    korrekt = P.lista[i].namn
    antal = 1
    print 'Vilken beteckning har grundämnet med nummer:', P.lista[i].anr , '?'
    gissa = raw_input('Vilken beteckning söker vi?')
    if gissa == korrekt:
        antal > 4

    while antal <= 2 :
        print 'feeeel!'
        gissa = raw_input('Vilken beteckning söker vi?')
        if gissa == korrekt:
            break
        antal = antal + 1
        
    print 'rätt svar var:', korrekt
        


def meny():
    
    while True:
        print '1. Visa alla atomer'
        print '2. Träna på atomnummer'
        print '3. Träna på atombeteckning'
        print '4. Sluta\n'
        x = raw_input('Vad vill du göra?: \n')
        try:
            
            if x == '1':
                P.skriv_ut()
                
            elif x == '2':
                trana_anr()
                
            elif x == '3':
                trana_namn()
               
            elif x == '4':
                print 'HEJDÅ!'
                break
                
            else:
                print 'Gör om gör rätt!'

        except ValueError:
            print 'Gör om gör rätt'


#HP:
print 'HEJ OCH VÄLKOMMEN!\n'
P = Periodiska_Systemet()
P.lasa_in()
P.sortering()
P.undantag()
P.atom_numrering()
meny()




       

    

