# Tűzoltóság Szimuláció

Ez a program egy szórakoztató és edukatív tűzoltóság szimulációs rendszer, amelyben egy városban tűzesetek keletkeznek, és a tűzoltók tűzoltóautókkal kivonulnak a helyszínre, hogy eloltsák a tüzet. A program célja, hogy bemutassa a tűzoltás és a vízhasználat logikáját, valamint lehetőséget adjon a felhasználóknak arra, hogy irányítsák a tűzoltás folyamatát.

Funkcionalitások
Város kezelése: Az alkalmazás egy virtuális várost szimulál, amelyben több épület, tűzoltó és tűzoltóautó található.

Felhasználói interakció: A felhasználó megadhatja, hogy hol keletkezik tűz és milyen típusú tüzet kell oltani.

Automatikus tűzoltás: A rendszer automatikusan kiválaszt egy szabad tűzoltót és egy tűzoltóautót, amelyek gyorsan reagálnak, kivonulnak a helyszínre, és eloltják a tüzet.

Vízhasználat: A tűzoltás során egy megadott vízforrásból fogyaszt vizet a rendszer. A vízkészlet fogyása hatással van a tűzoltás hatékonyságára.

Körbefutó program: A program folyamatosan fut, amíg a felhasználó nem dönt úgy, hogy kilép.

Használat
Indítás:
A program a Program.cs fájl futtatásával indul.

Tűzeset megadása:
A program bekéri a felhasználótól:

Melyik épületben keletkezik tűz
Milyen típusú tüzet kell oltani
Tűzoltás folyamata:
A rendszer keres egy szabad tűzoltót és egy tűzoltóautót.
A tűzoltóautó kivonul a helyszínre.
A tűzoltó eloltja a tüzet a megfelelő módszerrel.
A tűzoltóautó visszatér az állomásra.
Kilépés:
Ha a felhasználó 0-t ír be, a program befejeződik, és a szimuláció véget ér.

Tűztípusok
Közönséges tűz: Fa, papír, textil stb. (a leggyakoribb tűzesetek)
Olajos tűz: Benzin, étolaj, gyúlékony folyadékok
Elektromos tűz: Áramütés, elektromos vezetékek vagy berendezések tüzei
Példa futtatás:
A felhasználó megadja, hogy a tűz egy épületben tört ki, például egy irodaházban.
A program kiválaszt egy szabad tűzoltót és tűzoltóautót.
A megfelelő vízhasználattal eloltják a tüzet.
