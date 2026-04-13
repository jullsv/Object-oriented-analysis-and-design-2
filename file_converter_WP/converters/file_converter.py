import os
from elements.file import File
from elements.image_file import ImageFile
from elements.document_file import DocumentFile

class FileConverter:
    def convert_to_jpeg(self, file: File, output_path: str, quality: int = 90) -> None:
        filename = os.path.basename(file.path)
        name_without_ext = os.path.splitext(filename)[0]
        output_file = os.path.join(output_path, f"{name_without_ext}.jpg")
        
        if isinstance(file, ImageFile):
            print(f"Конвертация изображения {file.path} в JPEG")
            print(f"   Параметры: {file.width}x{file.height}, формат {file.format}")
            print(f"   Качество: {quality}%")
            print(f"   Сохранение в: {output_file}")
            
            try:
                from PIL import Image
                img = Image.open(file.path)
                if img.mode in ('RGBA', 'P'):
                    img = img.convert('RGB')
                img.save(output_file, 'JPEG', quality=quality)
                print(f"   Изображение конвертировано в JPEG")
            except ImportError:
                print("   Установите Pillow: pip install Pillow")
                with open(output_file, 'w', encoding='utf-8') as f:
                    f.write(f"JPEG конвертация: {file.path}\n")
                    f.write(f"Качество: {quality}%\n")
                print(f"   Создан файл: {output_file}")
            except Exception as e:
                print(f"   Ошибка: {e}")
        
        elif isinstance(file, DocumentFile):
            print(f"Конвертация документа {file.path} в JPEG")
            print(f"   Параметры: {file.pages} стр., {file.word_count} слов")
            print(f"   Качество: {quality}%")
            print(f"   Сохранение в: {output_file}")
            
            with open(output_file, 'w', encoding='utf-8') as f:
                f.write(f"Документ конвертирован в JPEG\n")
                f.write(f"Исходный файл: {file.path}\n")
                f.write(f"Страниц: {file.pages}\n")
                f.write(f"Слов: {file.word_count}\n")
                f.write(f"Качество: {quality}%\n")
            
            print(f"   Создан файл: {output_file}")
    
    def convert_to_png(self, file: File, output_path: str, compression: int = 6) -> None:
        filename = os.path.basename(file.path)
        name_without_ext = os.path.splitext(filename)[0]
        output_file = os.path.join(output_path, f"{name_without_ext}.png")
        
        if isinstance(file, ImageFile):
            print(f"Конвертация изображения {file.path} в PNG")
            print(f"   Параметры: {file.width}x{file.height}, формат {file.format}")
            print(f"   Компрессия: {compression}")
            print(f"   Сохранение в: {output_file}")
            
            try:
                from PIL import Image
                img = Image.open(file.path)
                img.save(output_file, 'PNG', compress_level=compression)
                print(f"   Изображение конвертировано в PNG")
            except ImportError:
                print("   Установите Pillow: pip install Pillow")
                with open(output_file, 'w', encoding='utf-8') as f:
                    f.write(f"PNG конвертация: {file.path}\n")
                    f.write(f"Компрессия: {compression}\n")
                print(f"   Создан файл: {output_file}")
            except Exception as e:
                print(f"   Ошибка: {e}")
        
        elif isinstance(file, DocumentFile):
            print(f"Конвертация документа {file.path} в PNG")
            print(f"   Параметры: {file.pages} стр., {file.word_count} слов")
            print(f"   Компрессия: {compression}")
            print(f"   Сохранение в: {output_file}")
            
            with open(output_file, 'w', encoding='utf-8') as f:
                f.write(f"Документ конвертирован в PNG\n")
                f.write(f"Исходный файл: {file.path}\n")
                f.write(f"Страниц: {file.pages}\n")
                f.write(f"Слов: {file.word_count}\n")
                f.write(f"Компрессия: {compression}\n")
            
            print(f"   Создан файл: {output_file}")
    
    def convert_to_pdf(self, file: File, output_path: str, page_size: str = "A4") -> None:
        filename = os.path.basename(file.path)
        name_without_ext = os.path.splitext(filename)[0]
        output_file = os.path.join(output_path, f"{name_without_ext}.pdf")
        
        if isinstance(file, ImageFile):
            print(f"Создание PDF из изображения {file.path}")
            print(f"   Параметры: {file.width}x{file.height}")
            print(f"   Размер страницы: {page_size}")
            print(f"   Сохранение в: {output_file}")
            
            try:
                from PIL import Image
                img = Image.open(file.path)
                img.save(output_file, 'PDF', resolution=100.0)
                print(f"   Изображение конвертировано в PDF")
            except ImportError:
                print("   Установите Pillow: pip install Pillow")
                with open(output_file, 'w', encoding='utf-8') as f:
                    f.write(f"PDF конвертация: {file.path}\n")
                    f.write(f"Размер страницы: {page_size}\n")
                print(f"   Создан файл: {output_file}")
            except Exception as e:
                print(f"   Ошибка: {e}")
        
        elif isinstance(file, DocumentFile):
            print(f"Конвертация документа {file.path} в PDF")
            print(f"   Параметры: {file.pages} стр., {file.word_count} слов")
            print(f"   Размер страницы: {page_size}")
            print(f"   Сохранение в: {output_file}")
            
            with open(output_file, 'w', encoding='utf-8') as f:
                f.write(f"Документ конвертирован в PDF\n")
                f.write(f"Исходный файл: {file.path}\n")
                f.write(f"Страниц: {file.pages}\n")
                f.write(f"Слов: {file.word_count}\n")
                f.write(f"Размер страницы: {page_size}\n")
            
            print(f"   Создан файл: {output_file}")