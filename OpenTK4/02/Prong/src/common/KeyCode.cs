using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Prong.common;

/// <summary>
/// O código a ser usado para identificar a entrada. Esses códigos correspondem diretamente a uma tecla física no teclado ou a teclas específicas do idioma.
/// </summary>
public enum KeyCode
{
    /// <summary>
    /// Não atribuído (nunca retornado como resultado de uma tecla pressionada).
    /// </summary>
    None = Keys.Unknown,

    /// <summary>
    /// A tecla Backspace.
    /// </summary>
    Backspace = Keys.Backspace,

    /// <summary>
    /// A tecla Delete para frente.
    /// </summary>
    Delete = Keys.Delete,

    /// <summary>
    /// A tecla Tab.
    /// </summary>
    Tab = Keys.Tab,

    /// <summary>
    /// A tecla Limpar.
    /// </summary>
    // Clear = Keys.,

    /// <summary>
    /// Tecla Enter.
    /// </summary>
    Return = Keys.Enter,

    /// <summary>
    /// Faça uma pausa nos computadores PC.
    /// </summary>
    Pause = Keys.Pause,

    /// <summary>
    /// Tecla Escape.
    /// </summary>
    Escape = Keys.Escape,

    /// <summary>
    /// Tecla Espaço.
    /// </summary>
    Space = Keys.Space,

    /// <summary>
    /// Teclado numérico 0.
    /// </summary>
    Keypad0 = Keys.KeyPad0,

    /// <summary>
    /// Teclado numérico 1.
    /// </summary>
    Keypad1 = Keys.KeyPad1,

    /// <summary>
    /// Teclado numérico 2.
    /// </summary>
    Keypad2 = Keys.KeyPad2,

    /// <summary>
    /// Teclado numérico 3.
    /// </summary>
    Keypad3 = Keys.KeyPad3,

    /// <summary>
    /// Teclado numérico 4.
    /// </summary>
    Keypad4 = Keys.KeyPad4,

    /// <summary>
    /// Teclado numérico 5.
    /// </summary>
    Keypad5 = Keys.KeyPad5,

    /// <summary>
    /// Teclado numérico 6.
    /// </summary>
    Keypad6 = Keys.KeyPad6,

    /// <summary>
    /// Teclado numérico 7.
    /// </summary>
    Keypad7 = Keys.KeyPad7,

    /// <summary>
    /// Teclado numérico 8.
    /// </summary>
    Keypad8 = Keys.KeyPad8,

    /// <summary>
    /// Teclado numérico 9.
    /// </summary>
    Keypad9 = Keys.KeyPad9,

    /// <summary>
    /// Teclado numérico '.'.
    /// </summary>
    KeypadPeriod = Keys.KeyPadDecimal,

    /// <summary>
    /// Teclado numérico '/'.
    /// </summary>
    KeypadDivide = Keys.KeyPadDivide,

    /// <summary>
    /// Teclado numérico '*'.
    /// </summary>
    KeypadMultiply = Keys.KeyPadMultiply,

    /// <summary>
    /// Teclado numérico '-'.
    /// </summary>
    KeypadMinus = Keys.KeyPadSubtract,

    /// <summary>
    /// Teclado numérico '+'.
    /// </summary>
    KeypadPlus = Keys.KeyPadAdd,

    /// <summary>
    /// Teclado numérico Enter.
    /// </summary>
    KeypadEnter = Keys.KeyPadEnter,

    /// <summary>
    /// Tecla de seta para cima.
    /// </summary>
    UpArrow = Keys.Up,

    /// <summary>
    /// Tecla de seta para baixo.
    /// </summary>
    DownArrow = Keys.Down,

    /// <summary>
    /// Tecla de seta para a direita.
    /// </summary>
    RightArrow = Keys.Right,

    /// <summary>
    /// Tecla de seta para a esquerda.
    /// </summary>
    LeftArrow = Keys.Left,

    /// <summary>
    /// Inserir tecla.
    /// </summary>
    Insert = Keys.Insert,

    /// <summary>
    /// Tecla inicial.
    /// </summary>
    Home = Keys.Home,

    /// <summary>
    /// Tecla final.
    /// </summary>
    End = Keys.End,

    /// <summary>
    /// Página acima.
    /// </summary>
    PageUp = Keys.PageUp,

    /// <summary>
    /// Role a página para baixo.
    /// </summary>
    PageDown = Keys.PageDown,

    /// <summary>
    /// Tecla de função F1.
    /// </summary>
    F1 = Keys.F1,

    /// <summary>
    /// Tecla de função F2.
    /// </summary>
    F2 = Keys.F2,

    /// <summary>
    /// Tecla de função F3.
    /// </summary>
    F3 = Keys.F3,

    /// <summary>
    /// Tecla de função F4.
    /// </summary>
    F4 = Keys.F4,

    /// <summary>
    /// Tecla de função F5.
    /// </summary>
    F5 = Keys.F5,

    /// <summary>
    /// Tecla de função F6.
    /// </summary>
    F6 = Keys.F6,

    /// <summary>
    /// Tecla de função F7.
    /// </summary>
    F7 = Keys.F7,

    /// <summary>
    /// Tecla de função F8.
    /// </summary>
    F8 = Keys.F8,

    /// <summary>
    /// Tecla de função F9.
    /// </summary>
    F9 = Keys.F9,

    /// <summary>
    /// Tecla de função F10.
    /// </summary>
    F10 = Keys.F10,

    /// <summary>
    /// Tecla de função F11.
    /// </summary>
    F11 = Keys.F11,

    /// <summary>
    /// Tecla de função F12.
    /// </summary>
    F12 = Keys.F12,

    /// <summary>
    /// A tecla '0' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha0 = Keys.D0,

    /// <summary>
    /// A tecla '1' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha1 = Keys.D1,

    /// <summary>
    /// A tecla '2' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha2 = Keys.D2,

    /// <summary>
    /// A tecla '3' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha3 = Keys.D3,

    /// <summary>
    /// A tecla '4' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha4 = Keys.D4,

    /// <summary>
    /// A tecla '5' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha5 = Keys.D5,

    /// <summary>
    /// A tecla '6' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha6 = Keys.D6,

    /// <summary>
    /// A tecla '7' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha7 = Keys.D7,

    /// <summary>
    /// A tecla '8' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha8 = Keys.D8,

    /// <summary>
    /// A tecla '9' na parte superior do teclado alfanumérico.
    /// </summary>
    Alpha9 = Keys.D9,

    /// <summary>
    /// Chave de citação '.
    /// </summary>
    Quote = Keys.GraveAccent,

    /// <summary>
    /// Chave vírgula ','.
    /// </summary>
    Comma = Keys.Comma,

    /// <summary>
    /// Tecla '-'.
    /// </summary>
    Minus = Keys.Minus,

    /// <summary>
    /// Tecla de ponto final '.'.
    /// </summary>
    Period = Keys.Period,

    /// <summary>
    /// Tecla de barra '/'.
    /// </summary>
    Slash = Keys.Slash,

    /// <summary>
    /// Tecla ponto e vírgula ';'.
    /// </summary>
    Semicolon = Keys.Semicolon,

    /// <summary>
    /// Tecla de igualdade '='.
    /// </summary>
    Equals = Keys.Equal,

    /// <summary>
    /// Chave de colchete esquerdo '['.
    /// </summary>
    LeftBracket = Keys.LeftBracket,

    /// <summary>
    /// Tecla de barra invertida '\'.
    /// </summary>
    Backslash = Keys.Backslash,

    /// <summary>
    /// Chave de colchete direito ']'.
    /// </summary>
    RightBracket = Keys.RightBracket,

    /// <summary>
    /// Tecla de crase '`'.
    /// </summary>
    BackQuote = Keys.GraveAccent,

    /// <summary>
    /// tecla 'a'.
    /// </summary>
    A = Keys.A,

    /// <summary>
    /// tecla 'b'.
    /// </summary>
    B = Keys.B,

    /// <summary>
    /// tecla 'c'.
    /// </summary>
    C = Keys.C,

    /// <summary>
    /// tecla 'd'.
    /// </summary>
    D = Keys.D,

    /// <summary>
    /// tecla 'e'.
    /// </summary>
    E = Keys.E,

    /// <summary>
    /// tecla 'f'.
    /// </summary>
    F = Keys.F,

    /// <summary>
    /// tecla 'g'.
    /// </summary>
    G = Keys.G,

    /// <summary>
    /// tecla 'h'.
    /// </summary>
    H = Keys.H,

    /// <summary>
    /// tecla 'i'.
    /// </summary>
    I = Keys.I,

    /// <summary>
    /// tecla 'j'.
    /// </summary>
    J = Keys.J,

    /// <summary>
    /// tecla 'k'.
    /// </summary>
    K = Keys.K,

    /// <summary>
    /// tecla 'l'.
    /// </summary>
    L = Keys.L,

    /// <summary>
    /// tecla 'm'.
    /// </summary>
    M = Keys.M,

    /// <summary>
    /// tecla 'n'.
    /// </summary>
    N = Keys.N,

    /// <summary>
    /// tecla 'o'.
    /// </summary>
    O = Keys.O,

    /// <summary>
    /// tecla 'p'.
    /// </summary>
    P = Keys.P,

    /// <summary>
    /// tecla 'q'.
    /// </summary>
    Q = Keys.Q,

    /// <summary>
    /// tecla 'r'.
    /// </summary>
    R = Keys.R,

    /// <summary>
    /// tecla 's'.
    /// </summary>
    S = Keys.S,

    /// <summary>
    /// tecla 't'.
    /// </summary>
    T = Keys.T,

    /// <summary>
    /// tecla 'u'.
    /// </summary>
    U = Keys.U,

    /// <summary>
    /// tecla 'v'.
    /// </summary>
    V = Keys.V,

    /// <summary>
    /// tecla 'w'.
    /// </summary>
    W = Keys.W,

    /// <summary>
    /// tecla 'x'.
    /// </summary>
    X = Keys.X,

    /// <summary>
    /// tecla 'y'.
    /// </summary>
    Y = Keys.Y,

    /// <summary>
    /// tecla 'z'.
    /// </summary>
    Z = Keys.Z,

    /// <summary>
    /// Tecla Num Lock.
    /// </summary>
    Numlock = Keys.NumLock,

    /// <summary>
    /// Tecla Caps Lock.
    /// </summary>
    CapsLock = Keys.CapsLock,

    /// <summary>
    /// Tecla Scroll Lock.
    /// </summary>
    ScrollLock = Keys.ScrollLock,

    /// <summary>
    /// Tecla Shift direita.
    /// </summary>
    RightShift = Keys.RightShift,

    /// <summary>
    /// Tecla Shift esquerda.
    /// </summary>
    LeftShift = Keys.LeftShift,

    /// <summary>
    /// Tecla Control direita.
    /// </summary>
    RightControl = Keys.RightControl,

    /// <summary>
    /// Tecla Control esquerda.
    /// </summary>
    LeftControl = Keys.LeftControl,

    /// <summary>
    /// Tecla Alt direita.
    /// </summary>
    RightAlt = Keys.RightAlt,

    /// <summary>
    /// Tecla Alt esquerda.
    /// </summary>
    LeftAlt = Keys.LeftAlt,

    /// <summary>
    /// Mapeia para a tecla Windows esquerda ou para a tecla Command esquerda se as teclas físicas estiverem ativadas nas configurações do Gerenciador de Entrada; caso contrário, mapeia apenas para a tecla Command esquerda.
    /// </summary>
    LeftMeta = Keys.LeftSuper,

    /// <summary>
    /// Mapeia para a tecla Windows direita ou para a tecla Command direita se as teclas físicas estiverem ativadas nas configurações do Gerenciador de Entrada; caso contrário, mapeia apenas para a tecla Command direita.
    /// </summary>
    RightMeta = Keys.RightSuper,

    /// <summary>
    /// Imprimir chave.
    /// </summary>
    Print = Keys.PrintScreen,

    /// <summary>
    /// Tecla de menu.
    /// </summary>
    Menu = Keys.Menu,

    /// <summary>
    /// O botão esquerdo (ou principal) do mouse.
    /// </summary>
    Mouse0 = MouseButton.Button1,

    /// <summary>
    /// Botão direito do mouse (ou botão secundário do mouse).
    /// </summary>
    Mouse1 = MouseButton.Button2,

    /// <summary>
    /// Botão do meio do mouse (ou terceiro botão).
    /// </summary>
    Mouse2 = MouseButton.Button3,
}
