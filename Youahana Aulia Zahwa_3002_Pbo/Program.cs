using System;
using System.Collections.Generic;

public class Karyawan
{
    public string nama { get; set; }
    public double gaji { get; set; }

    public Karyawan(string nama, double gaji)
    {
        this.nama = nama;
        this.gaji = gaji;
    }

    public virtual void Kerja()
    {
        Console.WriteLine($"{nama} sedang bekerja.");
    }

    public virtual void InfoKaryawan()
    {
        Console.WriteLine($"Nama   : {nama}");
        Console.WriteLine($"Gaji   : Rp{gaji:N0}");
    }
}

public class Tetap : Karyawan
{
    public double tunjangan { get; set; }

    public Tetap(string nama, double gaji, double tunjangan) : base(nama, gaji)
    {
        this.tunjangan = tunjangan;
    }

    public double HitungGajiTotal()
    {
        return gaji + tunjangan;
    }

    public override void Kerja()
    {
        Console.WriteLine($"{nama} bekerja sebagai karyawan tetap dengan jam kerja penuh waktu.");
    }

    public override void InfoKaryawan()
    {
        base.InfoKaryawan();
        Console.WriteLine($"Tunjangan  : Rp{tunjangan:N0}");
        Console.WriteLine($"Gaji Total : Rp{HitungGajiTotal():N0}");
    }
}

public class Kontrak : Karyawan
{
    public int durasi { get; set; }

    public Kontrak(string nama, double gaji, int durasi) : base(nama, gaji)
    {
        this.durasi = durasi;
    }

    public void CekKontrak()
    {
        Console.WriteLine($"Kontrak {nama} berlaku selama {durasi} bulan.");
    }

    public override void Kerja()
    {
        Console.WriteLine($"{nama} bekerja sebagai karyawan kontrak selama masa kontrak.");
    }

    public override void InfoKaryawan()
    {
        base.InfoKaryawan();
        Console.WriteLine($"Durasi : {durasi} bulan");
    }
}

public class Manager : Tetap
{
    public Manager(string nama, double gaji, double tunjangan) : base(nama, gaji, tunjangan) { }

    public override void Kerja()
    {
        Console.WriteLine($"{nama} bekerja sebagai manager yang mengatur dan mengawasi pekerjaan tim.");
    }

    public void Memimpin()
    {
        Console.WriteLine($"{nama} sedang memimpin rapat tim.");
    }
}

public class Staff : Tetap
{
    public Staff(string nama, double gaji, double tunjangan) : base(nama, gaji, tunjangan) { }

    public override void Kerja()
    {
        Console.WriteLine($"{nama} bekerja sebagai staff mengerjakan tugas operasional harian.");
    }

    public void KerjakanTugas()
    {
        Console.WriteLine($"{nama} sedang mengerjakan tugas yang diberikan.");
    }
}

public class Magang : Kontrak
{
    public Magang(string nama, double gaji, int durasi) : base(nama, gaji, durasi) { }

    public override void Kerja()
    {
        Console.WriteLine($"{nama} bekerja sebagai karyawan magang sambil belajar dengan bimbingan mentor.");
    }

    public void Belajar()
    {
        Console.WriteLine($"{nama} sedang belajar hal baru di perusahaan.");
    }
}

public class Freelancer : Kontrak
{
    public Freelancer(string nama, double gaji, int durasi) : base(nama, gaji, durasi) { }

    public override void Kerja()
    {
        Console.WriteLine($"{nama} bekerja sebagai freelancer dan mengerjakan proyek secara mandiri dari mana saja.");
    }

    public void AmbilProyek()
    {
        Console.WriteLine($"{nama} mengambil proyek baru.");
    }
}

public class Perusahaan
{
    public List<Karyawan> daftarKaryawan = new List<Karyawan>();

    public void TambahKaryawan(Karyawan karyawan)
    {
        daftarKaryawan.Add(karyawan);
        Console.WriteLine($"{karyawan.nama} berhasil ditambahkan ke perusahaan.");
    }

    public void DaftarKaryawan()
    {
        Console.WriteLine("\nDAFTAR KARYAWAN");
        int no = 1;
        foreach (Karyawan k in daftarKaryawan)
        {
            Console.WriteLine($"\n[{no++}] {k.GetType().Name}");
            k.InfoKaryawan();
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Perusahaan perusahaan = new Perusahaan();

        Manager manager = new Manager("Wawa", 15000000, 5000000);
        Staff staff = new Staff("Caca", 7000000, 1500000);
        Magang magang = new Magang("Rayya", 2000000, 6);
        Freelancer freelancer = new Freelancer("Ayyun", 5000000, 3);

        perusahaan.TambahKaryawan(manager);
        perusahaan.TambahKaryawan(staff);
        perusahaan.TambahKaryawan(magang);
        perusahaan.TambahKaryawan(freelancer);

        perusahaan.DaftarKaryawan();

        Console.WriteLine("\nPOLYMORPHISM: Kerja()");
        foreach (Karyawan k in perusahaan.daftarKaryawan)
        {
            k.Kerja();
        }

        Console.WriteLine("\nMETHOD KHUSUS");
        manager.Memimpin();
        staff.KerjakanTugas();
        magang.Belajar();
        freelancer.AmbilProyek();
        magang.CekKontrak();
        freelancer.CekKontrak();
        Console.WriteLine($"Gaji total {manager.nama}: Rp{manager.HitungGajiTotal():N0}");

        Console.WriteLine("\nSOAL 1: Kerja() pada Manager dan Freelancer");
        manager.Kerja();
        freelancer.Kerja();

        Console.WriteLine("\nSOAL 2: Memimpin() pada Manager");
        manager.Memimpin();

        Console.WriteLine("\nSOAL 3: Info lengkap Manager");
        manager.InfoKaryawan();

        Console.WriteLine("\nSOAL 4: Belajar() pada Magang");
        magang.Belajar();

        Console.WriteLine("\nSOAL 5: Variabel Karyawan berisi objek Staff");
        Karyawan k1 = new Staff("Aulia", 6500000, 1000000);
        k1.Kerja();
        ((Staff)k1).KerjakanTugas();

        Console.ReadKey();
    }
}